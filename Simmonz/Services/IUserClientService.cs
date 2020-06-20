using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Services
{
    public interface IUserClientService
    {
        Task<ApiResult<string>> Authentication(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
    }
}
