using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
