using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<List<UserViewModel>> GetAll();
        Task<ApiResult<PagedResult<UserViewModel>>> GetAllPaging(GetUserPagingRequest request);
        Task<ApiResult<UserViewModel>> GetById(int id);
        Task<ApiResult<bool>> RoleAssign(int id, RoleAssignRequest request);
        Task<ApiResult<bool>> Update(UserUpdateRequest request);
        Task<ApiResult<bool>> Delete(int id);
    }
}
