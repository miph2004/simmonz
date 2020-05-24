using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Simmonz.AdminSolution.Services;
using Simmonz.Data.Entities;

namespace Simmonz.AdminSolution.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleManager;
        public RoleController(IRoleService roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.GetAll();
            return Ok(roles);
        }
    }
}