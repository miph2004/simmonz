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
        public UserService(RoleManager<AppRole> roleManager,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,SimmonzDbContext context,IConfiguration config)
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
            if (user == null) return null;

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                return null;
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

        public async Task<List<UserViewModel>> GetAll()
        {
            var query = from p in _context.AppUsers
                        select new { p };
            var data = await query.Select(x => new UserViewModel()
            {
                Id=x.p.Id,
                FirstName=x.p.FirstName,
                LastName=x.p.LastName,
                DayOfBirth=x.p.DayOfBirth,
                Email=x.p.Email,
                PhoneNumber=x.p.PhoneNumber,
            }).ToListAsync();
            return data;
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
                LastName=request.LastName,
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
    }
}
