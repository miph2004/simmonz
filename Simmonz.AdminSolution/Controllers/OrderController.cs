using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simmonz.AdminSolution.Helpers;
using Simmonz.AdminSolution.Services.Order;
using Simmonz.ViewModel.Order;

namespace Simmonz.AdminSolution.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        public OrderController (IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index(int keyword,int pageIndex=1,int pageSize=2)
        {
            var request = new GetOrderPagingRequest()
            {
                pageIndex = pageIndex,
                Keyword = keyword,
                pageSize = pageSize,
            };
            ViewBag.KeyWord = keyword;
            var result = await _orderService.GetAllOrderPaging(request);
            if(result.IsSucceed)
            {
                return View(result.ResultObject);
            }    
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int orderId)
        {
            var result = await _orderService.Details(orderId);
            if(result.IsSucceed)
            {
                return View(result.ResultObject);
            }
            return BadRequest("Cannot find order");
        }
        [HttpGet]
        public async Task<IActionResult> GetTransaction(int transId)
        {
            var result = await _orderService.TransactionDetails(transId);
            if(result.IsSucceed)
            {
                return View(result.ResultObject);
            }
            return BadRequest("Cannot find transaction");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmStatus(int orderId ,int keyword,int pageIndex=1,int pageSize=2)
        {
            var result = await _orderService.ConfirmStatus(orderId);
            var request = new GetOrderPagingRequest()
            {
                pageIndex = pageIndex,
                Keyword = keyword,
                pageSize = pageSize,
            };  
            var model= await _orderService.GetAllOrderPaging(request);
            if (result.IsSucceed)
            {
                var ajax = new { html = JsonHelper.RenderRazorViewToString(this, "_ViewAll", model.ResultObject) };
                return Json(ajax);
            }
            ModelState.AddModelError("", result.Message);
            return View(result.Message);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeliverStatus(int orderId, int keyword, int pageIndex = 1, int pageSize = 2)
        {
            var result = await _orderService.DeliverStatus(orderId);
            var request = new GetOrderPagingRequest()
            {
                Keyword = keyword,
                pageIndex = pageIndex,
                pageSize = pageSize,
            };
            var model = await _orderService.GetAllOrderPaging(request);
            if(result.IsSucceed)
            {
                var ajax = new { html = JsonHelper.RenderRazorViewToString(this, "_ViewAll", model.ResultObject) };
                return Json(ajax);
            }
            ModelState.AddModelError("", result.Message);
            return View(result.Message);
        }
    }
}