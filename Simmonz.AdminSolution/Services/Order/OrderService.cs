using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Simmonz.Data.EF;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services.Order
{
    public class OrderService : IOrderService
    {
        private readonly SimmonzDbContext _context;
        public OrderService (SimmonzDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<PagedResult<OrderViewModel>>> GetAllOrderPaging(GetOrderPagingRequest request)
        {
            var query = from p in _context.Orders
                        select new { p };
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                                    .Take(request.pageSize)
                                    .Select(x => new OrderViewModel()
                                    {
                                        Id=x.p.Id,
                                        Amount=x.p.Amount,
                                        Status=x.p.Status,
                                        Discount=x.p.Discount,
                                        ProductId=x.p.ProductId,
                                        Quantity=x.p.Quantity,
                                        TransactionId=x.p.TransactionId,
                                    }
                                    ).ToListAsync();
            var pagedResult = new PagedResult<OrderViewModel>()
            {
                TotalRecords=totalRow,
                pageSize=request.pageSize,
                pageIndex=request.pageIndex,
                Items=data,
            };
            return new ApiSuccessResult<PagedResult<OrderViewModel>>(pagedResult);
        }
    }
}
