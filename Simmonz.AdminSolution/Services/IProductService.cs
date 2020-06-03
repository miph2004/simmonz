using Simmonz.Data.Entities;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public interface IProductService
    {
        Task<ApiResult<PagedResult<ProductViewModel>>> GetAllPaging(GetProductPagingRequest request);
        Task<List<ProductViewModel>> GetAll( );
        Task<ApiResult<PagedResult<ProductViewModel>>> GetAjax(GetProductPagingRequest request);

        Task<ApiResult<bool>> CreateProduct(ProductCreateRequest request,string fileName);
        Task<ApiResult<ProductViewModel>> GetProductById(int productId);
        Task<ApiResult<bool>> UpdateProduct(ProductUpdateRequest request,string fileName);
        Task<ApiResult<bool>> DeleteProduct(int productId);
    }
}
