using Microsoft.EntityFrameworkCore;
using Simmonz.Data.EF;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Services
{
    public class ProductClientService : IProductClientService
    {
        private readonly SimmonzDbContext _context;
        public ProductClientService (SimmonzDbContext context)
        {
            _context = context;
        }

       
        public async Task<ApiResult<List<ProductViewModel>>> GetAllProduct(string keyword,int categoryId, decimal minPrice, decimal maxPrice)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                           select new { p,c };
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.p.ProductName.Contains(keyword));
            }    
            if(categoryId!=0)
            {
                query = query.Where(x => x.c.Id.Equals(categoryId));
            }    
            if(minPrice>0&&maxPrice>0)
            {
                query = query.Where(x => x.p.Price >= minPrice & x.p.Price <= maxPrice);
            }    
            var products = await query.Select(x => new ProductViewModel()
            {
                Id=x.p.Id,
                ProductName=x.p.ProductName,
                CategoryName=x.c.Name,
                Description=x.p.Description,
                Image=x.p.Image,
                Price=x.p.Price,
                Quantity=x.p.Quantity,

            }).ToListAsync();
            return new ApiSuccessResult<List<ProductViewModel>>(products);
        }

        public async Task<ApiResult<ProductViewModel>> GetFilteredProduct(string keyword, int categoryId, decimal minPrice, decimal maxPrice)
        {
            var query =  _context.Products.Select(x=>x);
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.ProductName.Contains(keyword));
            }
            if (categoryId != 0)
            {
                query = query.Where(x => x.CategoryId.Equals(categoryId));
            }
            if (minPrice > 0 && maxPrice > 0)
            {
                query = query.Where(x => x.Price >= minPrice & x.Price <= maxPrice);
            }
            
            var product = new ProductViewModel()
            {
                ProductName = "",
            };
            return new ApiSuccessResult<ProductViewModel>(product);
        }

        public async Task<ApiResult<ProductViewModel>> GetProductById(int productId)
        {
            var product = await _context.Products.Include(c => c.Category).FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null) return new ApiErrorResult<ProductViewModel>($"Không tìm thấy sản phẩm có mã {productId}");
            var result = new ProductViewModel()
            {
                Id=product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryName = product.Category.Name,
                Description = product.Description,
                Image = product.Image,
                Quantity = product.Quantity,
            };
            return new ApiSuccessResult<ProductViewModel>(result);
        }
    }
}
