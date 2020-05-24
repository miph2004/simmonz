using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simmonz.AdminSolution.Services;
using Simmonz.Data.Entities;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Product;
using Simmonz.ViewModel.User;

namespace Simmonz.AdminSolution.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;

        }
        public async Task<IActionResult> Index(string keyword,int pageIndex = 1,int pageSize=1)
        {
            var request = new GetUserPagingRequest()
            {
                Keyword = keyword,
                pageIndex = pageIndex,
                pageSize = pageSize,
            };
            var result = await _userService.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            else if (TempData["resultFalied"]!=null)
            {
                ViewBag.FailedMsg = TempData["resultFalied"];
            }    
            return View(result.ResultObject);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userService.Register(request);
            if (result.IsSucceed)
            {
                TempData["result"] = "Đăng ký user thành công";
                return RedirectToAction("Index");
            }
            TempData["resultFalied"] =result.Message;
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public async Task<IActionResult> RoleAssign(int id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userService.RoleAssign(request.Id, request);
            if(result.IsSucceed)
            {
                TempData["result"] = "Cập nhật quyền thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);

            return View(roleAssignRequest);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _userService.GetById(id);
            if (result.IsSucceed)
            {
                var user = result.ResultObject;
                var updateRequest = new UserUpdateRequest()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DayOfBirth = user.DayOfBirth,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Update(UserUpdateRequest request)
        {
            var result = await _userService.Update(request);
            if(result.IsSucceed)
            {

                TempData["result"] = "Cập nhật  thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _userService.GetById(id);
            if (result == null)
            {
                return BadRequest("Cannot find product");

            }
            return View(result.ResultObject);
        }
        [HttpPost]
        public async Task<IActionResult> Delete (int id)
        {
            var result = await _userService.Delete(id);
            if(result.IsSucceed)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            TempData["resultFailed"] = result.Message;
            return RedirectToAction("Index");
        }
        private async Task<RoleAssignRequest> GetRoleAssignRequest(int id)
        {
            var userObj = await _userService.GetById(id);
            var roleObj = await _roleService.GetAll();
            var roleAssignRequest = new RoleAssignRequest();
            foreach (var role in roleObj.ResultObject)
            {
                roleAssignRequest.Roles.Add(new SelectedItem()
                {
                    Id = role.Id,
                    Name = role.Name,
                    Selected = userObj.ResultObject.Roles.Contains(role.Name)
                }) ;
            }
            return roleAssignRequest;
        }
    }
}