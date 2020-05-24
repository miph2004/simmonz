using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simmonz.Data.Entities;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager; 
        }
        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleViewModel()
                {
                    Id=x.Id,
                    Name=x.Name,
                    Description=x.Description,
                }).ToListAsync();
            return new ApiSuccessResult<List<RoleViewModel>>(roles);
        }
    }
}
