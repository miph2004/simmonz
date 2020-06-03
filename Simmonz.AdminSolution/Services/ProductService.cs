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

        public async Task<ApiResult<bool>> CreateProduct(ProductCreateRequest request,string fileName)
        {
            var productResult = new Product()
            {
                ProductName = request.ProductName,
                Price = request.Price,
                DiscountId=request.DiscountId,
                CategoryId = request.CategoryId,
                Image = fileName,
                Description = request.Description,
                Quantity = request.Quantity,
                CreatedDate = DateTime.Now,
            };
            _context.Products.Add(productResult);
             await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            var query = from p in _context.Products
                        select new { p };
            var data = await query.Select(x => new ProductViewModel()
            {
                ProductName = x.p.ProductName,
                Price = x.p.Price,
                CategoryId = x.p.CategoryId,
                Quantity = x.p.Quantity,
                Description = x.p.Description,
            }).ToListAsync();
            return data;
        }

        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p, c };
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.p.ProductName.Contains(request.Keyword));


            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                 .Take(request.pageSize)
                 .Select(x => new ProductViewModel()
                 {
                     Id=x.p.Id,
                     ProductName = x.p.ProductName,
                     Price = x.p.Price,
                     CategoryName=x.c.Name,
                     Image=x.p.Image,
                     CategoryId = x.p.CategoryId,
                     DiscountId=x.p.DiscountId,
                     Quantity = x.p.Quantity,
                     Description = x.p.Description,
                 }).ToListAsync();


            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                pageIndex = request.pageIndex,
                pageSize = request.pageSize,
                Items = data,
                
            };
            return new ApiSuccessResult<PagedResult<ProductViewModel>>(pagedResult);
        }

        public async Task<ApiResult<ProductViewModel>> GetProductById(int productId)
        {
            var product = await _context.Products.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == productId);
            var productViewDetail = new ProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName=product.Category.Name,
                Description = product.Description,
                Image = product.Image,
                DiscountId=product.DiscountId,
                Quantity = product.Quantity,
            };
            return new ApiSuccessResult<ProductViewModel>(productViewDetail);
        }

        public async Task<ApiResult<bool>> UpdateProduct(ProductUpdateRequest request,string fileName)
        {
            var product = await _context.Products.FindAsync(request.Id);
            if (product == null) throw new Exception($"Cannot find product with id :{request.Id}");
            product.ProductName = request.ProductName;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.Image = fileName;
            product.Id = request.Id;
            product.CategoryId = request.CategoryId;
            product.DiscountId = request.DiscountId;
            product.Description = request.Description;
             await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<bool>> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return new ApiErrorResult<bool>("Không tìm thấy sản phẩm");
             _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<PagedResult<ProductViewModel>>> GetAjax(GetProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.Id
                        select new { p, c };
   

            var data = await query
                 .Select(x => new ProductViewModel()
                 {
                     Id = x.p.Id,
                     ProductName = x.p.ProductName,
                     Price = x.p.Price,
                     CategoryName = x.c.Name,
                     Image = x.p.Image,
                     CategoryId = x.p.CategoryId,
                     DiscountId = x.p.DiscountId,
                     Quantity = x.p.Quantity,
                     Description = x.p.Description,
                 }).ToListAsync();


            //4. Select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
               
                Items = data,

            };
            return new ApiSuccessResult<PagedResult<ProductViewModel>>(pagedResult);
        }
    }
}
