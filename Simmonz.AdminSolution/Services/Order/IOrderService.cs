using Simmonz.ViewModel;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Order;
using Simmonz.ViewModel.OrderDetail;
using Simmonz.ViewModel.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services.Order
{
    public interface IOrderService
    {
        Task<ApiResult<PagedResult<OrderViewModel>>> GetAllOrderPaging(GetOrderPagingRequest request);
        Task<ApiResult<bool>> ConfirmStatus(int orderId);
        Task<ApiResult<bool>> DeliverStatus(int orderId);
        Task<ApiResult<List<OrderDetailViewModel>>> Details(int orderId);
        Task<ApiResult<TransactionViewModel>> TransactionDetails(int transId);
    }
}
