using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Services
{
    public interface IProductClientService
    {
        Task<ApiResult<List<ProductViewModel>>> GetAllProduct(string keyword,int categoryId, decimal minPrice, decimal maxPrice);
        Task<ApiResult<ProductViewModel>> GetProductById(int productId);
        Task<ApiResult<ProductViewModel>> GetFilteredProduct(string keyword, int categoryId, decimal minPrice, decimal maxPrice);
    }
}
