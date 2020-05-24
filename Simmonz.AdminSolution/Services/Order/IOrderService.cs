using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services.Order
{
    public interface IOrderService
    {
        Task<ApiResult<PagedResult<OrderViewModel>>> GetAllOrderPaging(GetOrderPagingRequest request);
    }
}
