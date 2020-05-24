using Simmonz.ViewModel.Category;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public interface ICategoryService
    {
        Task<ApiResult<List<CategoryViewModel>>> GetAll();
        Task<ApiResult<CategoryViewModel>> GetCategoryNameByProductId(int productId);
    }
}
