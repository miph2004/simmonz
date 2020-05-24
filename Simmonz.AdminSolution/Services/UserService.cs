using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Simmonz.Data.EF;
using Simmonz.Data.Entities;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Product;
using Simmonz.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public class UserService : IUserService
    {
        //Inject thư viện của Identity
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly SimmonzDbContext _context;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public UserService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, SimmonzDbContext context, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _signInManager = signInManager;
            _config = config;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return new ApiErrorResult<string>("Tài khoản không tồn tại");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<string>("Sai mật khẩu");
            }
            var claims = new[]
             {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,request.UserName),
                new Claim(ClaimTypes.Name,user.FirstName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);
            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user == null)
            {
                return new ApiErrorResult<bool>($"Không tồn tại user có id :{id}");
            }
            var result = await _userManager.DeleteAsync(user);
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var query = from p in _context.AppUsers
                        select new { p };
            var data = await query.Select(x => new UserViewModel()
            {
                Id = x.p.Id,
                FirstName = x.p.FirstName,
                LastName = x.p.LastName,
                DayOfBirth = x.p.DayOfBirth,
                Email = x.p.Email,
                PhoneNumber = x.p.PhoneNumber,
            }).ToListAsync();
            return data;
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetAllPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.FirstName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }
            int totalRow = await query.CountAsync();
            //nếu pageIndex = 2 thì sẽ lược bỏ (2-1)*pageSize(=10)=10 bản ghi đầu
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                .Take(request.pageSize)
                .Select(x => new UserViewModel()
                {
                    FirstName = x.FirstName,
                    Email = x.Email,
                    Id = x.Id,
                    PhoneNumber = x.PhoneNumber,
                    LastName = x.LastName,
                    DayOfBirth = x.DayOfBirth,
                }).ToListAsync();
            var pagedResullt = new PagedResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                pageSize = request.pageSize,
                pageIndex = request.pageIndex,
                Items = data,
            };
            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResullt);
            
        }

        public async Task<ApiResult<UserViewModel>> GetById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return new ApiErrorResult<UserViewModel>($"Không tìm thấy tài khoản có mã {id} ");
            var roles = await _userManager.GetRolesAsync(user);
            var userVm = new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName=user.LastName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                DayOfBirth = user.DayOfBirth,
                Id = user.Id,
                Roles = roles
            };
            return new ApiSuccessResult<UserViewModel>(userVm);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");
            }
            if (await _userManager.FindByEmailAsync(request.Email) != null)
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            user = new AppUser()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                DayOfBirth = request.DayOfBirth,
                PhoneNumber = request.PhoneNumber,

            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> RoleAssign(int id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Tài khoản không tồn tại");
            }
            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x.Name).ToList();
            foreach(var roleName in removedRoles)
            {
                if(await _userManager.IsInRoleAsync(user,roleName)==true)
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }    
            }
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            var addedRoles = request.Roles.Where(x => x.Selected).Select(x => x.Name).ToList();
            foreach (var roleName in addedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, roleName) == false)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(UserUpdateRequest request)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != request.Id))
            {
                return new ApiErrorResult<bool>("Emai đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) throw new Exception($"Cannot find user with id :{request.Id}");
            user.LastName = request.LastName;
            user.FirstName = request.FirstName;
            user.Email = request.Email;
            user.DayOfBirth = request.DayOfBirth;
            user.PhoneNumber = request.PhoneNumber;
            var result = await _userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>();

        }
    }
}
