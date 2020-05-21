using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simmonz.AdminSolution.Services;
using Simmonz.Data.EF;
using Simmonz.ViewModel.Product;

namespace Simmonz.AdminSolution.Controllers
{
  
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    
        //http://localhost:port/product
       
        public async Task<IActionResult> Index()
        {
            var product = await _productService.GetAll();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            var result = await _productService.CreateProduct(request);
                return RedirectToAction("Index");
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> Details(int productId)
        {
            var result = await _productService.GetProductById(productId);
            if (result == null)
            {
                return BadRequest("Cannot find product");

            }
            return Ok(result);
        }
    }
}