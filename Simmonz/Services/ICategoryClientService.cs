using Simmonz.ViewModel.Category;
using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Services
{
     public interface ICategoryClientService 
    {
        Task<ApiResult<List<CategoryViewModel>>> GetAllCategory();
    }
}
