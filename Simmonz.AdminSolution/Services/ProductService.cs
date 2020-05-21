using Microsoft.EntityFrameworkCore;
using Simmonz.Data.EF;
using Simmonz.Data.Entities;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public class ProductService : IProductService
    {
        private readonly SimmonzDbContext _context;
        public ProductService(SimmonzDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateProduct(ProductCreateRequest request)
        {
            var product = new Product()
            {
                ProductName = request.ProductName,
                Price = request.Price,
                CategoryId = request.CategoryId,
                Image = request.Image,
                Description = request.Description,
                Quantity = request.Quantity,
                CreatedDate = DateTime.Now,
            };
            _context.Products.Add(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            var query = from p in _context.Products
                        select new { p };
            var data = await query.Select(x => new ProductViewModel()
            {
                ProductName=x.p.ProductName,
                Price=x.p.Price,
                CategoryId=x.p.CategoryId,
                Quantity=x.p.Quantity,
               Description=x.p.Description,
            }).ToListAsync();
            return data;
        }

        public Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductViewModel> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productViewDetail = new ProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Image = product.Image,
                Quantity = product.Quantity,
            };
            return productViewDetail;
        }
    }
}
