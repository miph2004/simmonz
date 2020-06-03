using Simmonz.Data.EF;
using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services.Chart
{
    public class ChartService : IChartService
    {
        private readonly SimmonzDbContext _context;
        public ChartService(SimmonzDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> GetData()
        {
            var products = from p in _context.Products
                           join c in _context.Categories on p.CategoryId equals c.Id
                           select new { p, c };
        
            return new ApiSuccessResult<bool>();
        }
    }
}
