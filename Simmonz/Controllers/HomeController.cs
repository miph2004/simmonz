using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Simmonz.AdminSolution.Helpers;
using Simmonz.Data.EF;
using Simmonz.Data.Entities;
using Simmonz.Model;
using Simmonz.Models;
using Simmonz.Services;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Order;
using Simmonz.ViewModel.Product;
using Simmonz.ViewModel.User;

namespace Simmonz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductClientService _productClientService;
        private readonly IOrderService _orderService;
        private readonly IUserClientService _userClientService;
        private readonly SimmonzDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICategoryClientService _categoryClientService;
        public HomeController(ILogger<HomeController> logger,
                              IProductClientService productClientService,
                              IOrderService orderService,
                              SimmonzDbContext context,
                              IUserClientService userClientService,
                              IConfiguration configuration,
                              ICategoryClientService categoryClientService
                              )
        {
            _categoryClientService = categoryClientService;
            _configuration = configuration;
            _userClientService = userClientService;
            _orderService = orderService;
            _productClientService = productClientService;
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string keyword,int categoryId, decimal minPrice, decimal maxPrice)
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }

            var result = await _productClientService.GetAllProduct(keyword, categoryId, minPrice,maxPrice);
            if (result.IsSucceed)
            {
                if (TempData["user"] != null)
                {
                    ViewBag.user = TempData["user"];
                }
                ViewBag.keyword = keyword;
                var catList = await _categoryClientService.GetAllCategory();
                ViewBag.Category = catList.ResultObject;
                ViewBag.Notify = TempData["result"];
                return View(result.ResultObject);
            }

            return BadRequest(result.Message);
        }
        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            string term = HttpContext.Request.Query["term"].ToString();
            
            var query = _context.Products.Where(p => p.ProductName.Contains(term)).Select(p=>p.ProductName).ToList();
           
            return Ok(query);
                

        }
        [HttpGet]
        public async Task<IActionResult> Shop(string keyword,int[] categoryId,decimal minPrice, decimal maxPrice)
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
           
            var result = await _productClientService.GetAllProduct(keyword, 0, minPrice,maxPrice);
            if(categoryId.Count()>0)
            {
                List<ProductViewModel> item = new List<ProductViewModel>();
                for (var i = 0; i <= categoryId.Count()-1; i++)
                {
                    result = await _productClientService.GetAllProduct(keyword, categoryId[i],minPrice,maxPrice);
                    foreach (var product in result.ResultObject)
                    {
                        item.Add(new ProductViewModel
                        {
                            ProductName=product.ProductName,
                            Price=product.Price,
                            CategoryId=product.CategoryId,
                            Description=product.Description,
                            CategoryName=product.CategoryName,
                            DiscountId=product.DiscountId,
                            Id=product.Id,
                            Image=product.Image,
                            Quantity=product.Quantity,
                        });
                    }
                    
                }
                ViewBag.filterCategory = "true";
                ViewBag.filterMinPrice = minPrice;
                ViewBag.filterMaxPrice = maxPrice;
                var catList = await _categoryClientService.GetAllCategory();
                ViewBag.Category = catList.ResultObject;
                ViewBag.keyword = keyword;
                return View(item);
            }    

            if (result.IsSucceed)
            {
                ViewBag.filterMinPrice = minPrice;
                ViewBag.filterMaxPrice = maxPrice;
                var catList = await _categoryClientService.GetAllCategory();
                ViewBag.Category = catList.ResultObject;
                ViewBag.keyword = keyword;
                return View(result.ResultObject);
            }
          
            return BadRequest(result.Message);
        }
        public async Task<IActionResult> Blog()
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            var catList = await _categoryClientService.GetAllCategory();
            ViewBag.Category = catList.ResultObject;
            return View();
        }
        public async Task<IActionResult> Contact()
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            var catList = await _categoryClientService.GetAllCategory();
            ViewBag.Category = catList.ResultObject;
            return View();
        }
        public async Task<IActionResult> BlogDetails()
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            var catList = await _categoryClientService.GetAllCategory();
            ViewBag.Category = catList.ResultObject;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if(TempData["registerSucceed"]!=null)
            {
                ViewBag.registered = TempData["registerSucceed"];
            }    
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            var catList = await _categoryClientService.GetAllCategory();
            ViewBag.Category = catList.ResultObject;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _userClientService.Authentication(request);
            if (result.IsSucceed)
            {
                var userPrincipal = this.ValidateToken(result.ResultObject);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    IsPersistent = false
                };
                HttpContext.Session.SetString("Tokens", result.ResultObject);
                await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            userPrincipal,
                            authProperties);
                HttpContext.Session.SetString("user", User.Identity.Name);
                var user = HttpContext.Session.GetString("user");
                TempData["user"] = user;
                var catList = await _categoryClientService.GetAllCategory();
                ViewBag.Category = catList.ResultObject;
                return RedirectToAction("Index", "Home");
            }
            TempData["resultFailed"] = result.Message;
            return RedirectToAction("Index");
        }
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Tokens");
            HttpContext.Session.Remove("user");
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Register( )
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            var catList = await _categoryClientService.GetAllCategory();
            ViewBag.Category = catList.ResultObject;



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if(request.Password!=request.ConfirmPassWord)
            {
                ViewBag.error = "Xác nhận mật khẩu không khớp";
                return View();
            }    
            var result = await _userClientService.Register(request);
            if(result.IsSucceed)
            {
                TempData["registerSucceed"] = "Đăng ký thành công";
                return RedirectToAction("Login");
            }
            ViewBag.invalid = result.Message;
            return View();
        }
        public async Task<IActionResult> ShoppingCart()
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
                return View();
            }
            ViewBag.cartCount = 0;
            ViewBag.cart = null;
            ViewBag.total = 0;
            var catList = await _categoryClientService.GetAllCategory();
            ViewBag.Category = catList.ResultObject;
            return View();
           
        }
        [HttpGet]
        [Route("addtocart/{productId}")]
        public async Task<IActionResult> AddToCart(int productId)
        {

            var result = await _productClientService.GetProductById(productId);
            var product = new Product()
            {
                ProductName = result.ResultObject.ProductName,
                Price = result.ResultObject.Price,
                CategoryId = result.ResultObject.CategoryId,
                Description = result.ResultObject.Description,
                Image = result.ResultObject.Image,
                Quantity = result.ResultObject.Quantity,
                Id = result.ResultObject.Id,

            };
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") == null)
            {       
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { Product = product, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                int index = isExist(productId);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Cart { Product = product, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("ShoppingCart");
        }
        [HttpPost]
        [Route("addtocart/{productId}")]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {

            var result = await _productClientService.GetProductById(productId);
            var product = new Product()
            {
                ProductName = result.ResultObject.ProductName,
                Price = result.ResultObject.Price,
                CategoryId = result.ResultObject.CategoryId,
                Description = result.ResultObject.Description,
                Image = result.ResultObject.Image,
                Quantity = result.ResultObject.Quantity,
                Id = result.ResultObject.Id,

            };
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { Product = product, Quantity = quantity });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                int index = isExist(productId);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Cart { Product = product, Quantity = quantity });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("ShoppingCart");
        }
        [Route("RemoveItemFromCart/{productId}")]
        public async Task<IActionResult> RemoveItemFromCart(int productId)
        {
            List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            int index = isExist(productId);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("ShoppingCart");
        }
        [HttpPost]
        [Route("UpdateCart")]
        public async Task<IActionResult> UpdateCart(int[] quantity)
        {
            
            List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            for(var i =0; i<cart.Count;i++)
            {
                cart[i].Quantity = quantity[i];
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("ShoppingCart");
        }
        private int isExist(int productId)
        {
            List<Cart> cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(productId))
                {
                    return i;
                }
            }
            return -1;
        }
        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            var catList = await _categoryClientService.GetAllCategory();
            ViewBag.Category = catList.ResultObject;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckOutRequest request)
        {
            List<Cart> cartRequest = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
            //Cart on Top

            //Define Function Checkout nè
            var transaction = new Transaction()
            {
                
                AdminId = 1,
                ClientId = 1,
                ShippingFeeId = 1,
                AddressDistrict=request.AddressDistrict,
                AddressStreet=request.AddressStreet,
                AddressNumber=request.AddressNumber,
                Message=request.Message,
                PaymentMethod=request.PaymentMethod,
                Status = 0,
                Amount = cartRequest.Sum(item => item.Product.Price * item.Quantity),
                CreatedDate = DateTime.Now,
            };
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            var order = new Order()
            {
                Amount = cartRequest.Sum(item => item.Product.Price * item.Quantity),
                Quantity=cartRequest.Count,
                
                CreatedDate = DateTime.Now,
                Status = 1,
                Discount = 1,
                TransactionId = transaction.Id,
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            foreach(var item in cartRequest)
            {     
                var orderDetail = new OrderDetail
                {
                    
                    ProductId=item.Product.Id,
                    Quantity=item.Quantity,
                    OrderId=order.Id,                    
                };
                _context.OrderDetails.Add(orderDetail);
                await _context.SaveChangesAsync();
            }
            HttpContext.Session.Remove("cart");
            TempData["result"] = "Thanks for buying, we are going to check your order ! ";
            return RedirectToAction("Index");
        }
        public IActionResult Faq()
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Product(int productId)
        {
            if (SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart") != null)
            {
                var cart = SessionHelper.GetObjectFromJson<List<Cart>>(HttpContext.Session, "cart");
                var count = cart.Count();
                ViewBag.cartCount = count;
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(item => item.Product.Price * item.Quantity);
            }
            var result = await _productClientService.GetProductById(productId);
            if (result.IsSucceed)
            {
                var catList = await _categoryClientService.GetAllCategory();
                ViewBag.Category = catList.ResultObject;
                return View(result.ResultObject);
            }
            return BadRequest(result.Message);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
