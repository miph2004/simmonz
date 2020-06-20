using Microsoft.EntityFrameworkCore;
using Simmonz.Data.EF;
using Simmonz.ViewModel.Category;
using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Services
{
    public class CategoryClientService : ICategoryClientService
    {
        private readonly SimmonzDbContext _context;
        public CategoryClientService(SimmonzDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<List<CategoryViewModel>>> GetAllCategory()
        {
            var query = from c in _context.Categories
                        orderby c.Name
                        select new { c };
            var cat = await query.Select(x => new CategoryViewModel()
            {
                Name = x.c.Name,
                Id=x.c.Id,
                Description=x.c.Description,

            }).ToListAsync();
            return new ApiSuccessResult<List<CategoryViewModel>>(cat);
        }
    }
}
