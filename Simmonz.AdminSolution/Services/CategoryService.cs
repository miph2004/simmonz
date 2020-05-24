using Microsoft.EntityFrameworkCore;
using Simmonz.Data.EF;
using Simmonz.ViewModel.Category;
using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SimmonzDbContext _context;
        public CategoryService(SimmonzDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<List<CategoryViewModel>>> GetAll()
        {
            var cats = await _context.Categories
               .Select(x => new CategoryViewModel()
               {
                   Id = x.Id,
                   Name = x.Name,
               }).ToListAsync();
            return new ApiSuccessResult<List<CategoryViewModel>>(cats);
        }

        public Task<ApiResult<CategoryViewModel>> GetCategoryNameByProductId(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
