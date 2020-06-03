using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simmonz.AdminSolution.Services;

namespace Simmonz.AdminSolution.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            if(result.IsSucceed)
            {
                ViewBag.count = result.ResultObject.Count();
                return View(result.ResultObject);
            }
            return View();
            
        }
    }
}