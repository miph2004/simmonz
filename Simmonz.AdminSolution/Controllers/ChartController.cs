using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simmonz.AdminSolution.Helpers;
using Simmonz.Data.EF;

namespace Simmonz.AdminSolution.Controllers
{

    public class ChartController : Controller
    {
        private readonly SimmonzDbContext _context;
        public ChartController(SimmonzDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();

        }
        public async Task<IActionResult> GetProducts()
        {
            var result = _context.Products
                         .Join(_context.Categories, p => p.CategoryId, ct => ct.Id, (p, ct) => new { p, ct })
                         .GroupBy(p => p.p.CategoryId)
                         .Select(g => new
                         {
                             Quantity = g.Sum(p => p.p.Quantity),
                             CatName = g.Key,
                         })
                         .OrderByDescending(q => q.Quantity)
                         .ToList();

            var labels = result.Select(p => p.CatName).ToArray();
            var values = result.Select(p => p.Quantity).ToArray();
            var max = values[0];
            List<object> listData = new List<object>();
            listData.Add(labels);
            listData.Add(values);
            listData.Add(max);
            return Json(listData);

        }
        public async Task<IActionResult> GetProfits()
        {

                var query = _context.Transactions
                          .GroupBy(t => t.CreatedDate.Month)
                         .Select(g => new
                         {
                             Month = g.Key,
                             Profit = g.Sum(a => a.Amount),
                         }).OrderBy(p => p.Month)
                         .ToList();   
               
            var labels = query.Select(m => m.Month).ToArray();
            var values = query.Select(p => p.Profit).ToArray();
            var max = values[0];
            List<object> listData = new List<object>();
            listData.Add(labels);
            listData.Add(values);
            listData.Add(max);
            return Json(listData);
        }
        [HttpGet]
        public async Task<IActionResult> GetRevenueByTime(int time)
        {
            if (time ==1)
            {
                DateTime givenDate = DateTime.Today;
                DateTime dateCriteria = givenDate.AddDays(-7);
                var query = _context.Transactions
                            .Where(t => t.CreatedDate > dateCriteria)
                          .GroupBy(t => t.CreatedDate)
                         .Select(g => new
                         {
                             Month = g.Key,
                             Profit = g.Sum(a => a.Amount),
                         }).OrderBy(p => p.Month)
                         .ToList();
                if (query.Count != 0)
                {
                    var labels = query.Select(m => m.Month.ToString("MM/dd/yyyy")).ToArray();
                    var values = query.Select(p => p.Profit).ToArray();
                    var max = values[0];
                    List<object> listData = new List<object>();
                    listData.Add(labels);
                    listData.Add(values);
                    listData.Add(max);
                    return Json(listData);
                }
                List<object> failed = new List<object>();
                failed.Add(0);
                failed.Add(0);
                failed.Add(0);
                return Json(failed);

            }
            else if (time == 2)
            {
                DateTime givenDate = DateTime.Today;
                DateTime startOfWeek = givenDate.AddDays(-1 * (int)givenDate.DayOfWeek);
                DateTime endOfWeek = startOfWeek.AddDays(7);
                var query = _context.Transactions
                            .Where(t => startOfWeek < t.CreatedDate && t.CreatedDate < endOfWeek)
                          .GroupBy(t => t.CreatedDate)
                         .Select(g => new
                         {
                             Month = g.Key,
                             Profit = g.Sum(a => a.Amount),
                         }).OrderBy(p => p.Month)
                         .ToList();
                if (query.Count!=0)
                {
                    var labels = query.Select(m => m.Month.ToString("MM/dd/yyyy")).ToArray();
                    var values = query.Select(p => p.Profit).ToArray();
                    var max = values[0];
                    List<object> listData = new List<object>();
                    listData.Add(labels);
                    listData.Add(values);
                    listData.Add(max);
                    return Json(listData);
                }
                List<object> failed = new List<object>();
                failed.Add(0);
                failed.Add(0);
                failed.Add(0);
                return Json(failed);


            }
            else if(time==3)
            {

                DateTime lastweek = DateTime.Now.Date.AddDays(-7);
                var query = _context.Transactions
                            .Where(t => t.CreatedDate > lastweek)
                          .GroupBy(t => t.CreatedDate)
                         .Select(g => new
                         {
                             Month = g.Key,
                             Profit = g.Sum(a => a.Amount),
                         }).OrderBy(p => p.Month)
                         .ToList();
                if (query.Count != 0)
                {
                    var labels = query.Select(m => m.Month.ToString("MM/dd/yyyy")).ToArray();
                    var values = query.Select(p => p.Profit).ToArray();
                    var max = values[0];
                    List<object> listData = new List<object>();
                    listData.Add(labels);
                    listData.Add(values);
                    listData.Add(max);
                    return Json(listData);
                }
                List<object> failed = new List<object>();
                failed.Add(0);
                failed.Add(0);
                failed.Add(0);
                return Json(failed);

            }
            else
            {
                var query = _context.Transactions
                         .GroupBy(t => t.CreatedDate.Month)
                        .Select(g => new
                        {
                            Month = g.Key,
                            Profit = g.Sum(a => a.Amount),
                        }).OrderBy(p => p.Month)
                        .ToList();

                var labels = query.Select(m => m.Month).ToArray();
                var values = query.Select(p => p.Profit).ToArray();
                var max = values[0];
                List<object> listData = new List<object>();
                listData.Add(labels);
                listData.Add(values);
                listData.Add(max);
                return Json(listData);
            }
        }
    }
}