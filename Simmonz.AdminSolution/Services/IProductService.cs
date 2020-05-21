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
        Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductRequest request);
        Task<List<ProductViewModel>> GetAll();

        Task<int> CreateProduct(ProductCreateRequest request);
        Task<ProductViewModel> GetProductById(int productId);
    }
}
