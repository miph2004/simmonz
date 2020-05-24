using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var result = await _orderService.GetAllOrderPaging(request);
            if(result.IsSucceed)
            {
                return View(result.ResultObject);
            }    
            return View();
        }
    }
}