using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Simmonz.AdminSolution.Services;
using Simmonz.Data.EF;
using Simmonz.Data.Entities;
using Simmonz.ViewModel.Product;

namespace Simmonz.AdminSolution.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductService _productService;
        private readonly SimmonzDbContext _context;
        public ProductController(IProductService productService,SimmonzDbContext context,IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
        }

        //http://localhost:port/product

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 2)
        {
            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                pageIndex = pageIndex,
                pageSize = pageSize
            };
            var data = await _productService.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data.ResultObject);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CategoryDropDownList();
            DiscountDropDownList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
                string uniqueFileName = null;
                var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath,"img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                request.Image.CopyTo(new FileStream(filePath, FileMode.Create));
            var fileName = uniqueFileName;
            
            var result = await _productService.CreateProduct(request, fileName);
            if (result.IsSucceed)
            {
                TempData["result"] = "Thêm mới người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        } 
        [HttpGet]
        public async Task<IActionResult> Details(int productId)
        {

            var result = await _productService.GetProductById(productId);
            if (result == null)
            {
                return BadRequest("Cannot find product");

            }
            return View(result.ResultObject);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int productId)
        {
            CategoryDropDownList();
            DiscountDropDownList();
            var result = await _productService.GetProductById(productId);
            if (result.IsSucceed)
            {
                var product = result.ResultObject;
                var updateRequest = new ProductUpdateRequest()
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    DiscountId=product.DiscountId,
                    Quantity = product.Quantity,
                };
                ViewBag.Image = product.Image;
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();
            string uniqueFileName = null;
            var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "img");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            var filePath = Path.Combine(uploadFolder, uniqueFileName);
            request.Image.CopyTo(new FileStream(filePath, FileMode.Create));
            var fileName = uniqueFileName;
            var result = await _productService.UpdateProduct(request, fileName);
            if (result.IsSucceed)
            {
                TempData["result"] = "Cập nhật người dùng thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if(result.IsSucceed)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result.Message);
            return View(result);
        }
        private void CategoryDropDownList(object seletedCategory = null)
        {
            var query = from c in _context.Categories
                                   orderby c.Name
                                   select c;
            ViewBag.CatList = new SelectList(query, "Id", "Name", seletedCategory);
        }
        private void DiscountDropDownList(object seletedDiscount = null)
        {
            var query = from c in _context.Discounts
                        orderby c.Name
                        select c;
            ViewBag.DiscountList = new SelectList(query, "Id", "Name", seletedDiscount);
        }
    }
}