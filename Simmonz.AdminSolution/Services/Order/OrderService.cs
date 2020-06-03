using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Simmonz.Data.EF;
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
    public class OrderService : IOrderService
    {
        private readonly SimmonzDbContext _context;
        public OrderService (SimmonzDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<bool>> ConfirmStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if(order==null)
            {
                return new ApiErrorResult<bool>($"Không tìm thấy đơn hàng có mã {orderId}");
            }
            if(order.Status == 0)
            {
                order.Status = 1;
            }    
            else if(order.Status == 1)
            {
                order.Status = 2;
            }    
            else if(order.Status==2)
            {
                order.Status = 3;
            }    
           
           await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
            

        }

        public async Task<ApiResult<bool>> DeliverStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
             if(order==null)
            {
                return new ApiErrorResult<bool>($"Không tìm thấy đơn hàng có mã {orderId}");
            }
            
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>();
        }

        public async Task<ApiResult<List<OrderDetailViewModel>>> Details(int orderId)
        {
            var query = from p in _context.OrderDetails
                        join c in _context.Products on p.Product.Id equals c.Id
                        where p.Order.Id==orderId
                        select new { p, c };
            var result = await query.Select(x => new OrderDetailViewModel()
            {
                ProductName=x.c.ProductName,
                Quantity=x.p.Quantity,
                Amount=x.c.Price* x.p.Quantity,
                
            }).ToListAsync();
            return new ApiSuccessResult<List<OrderDetailViewModel>>(result);
        }

        public async Task<ApiResult<PagedResult<OrderViewModel>>> GetAllOrderPaging(GetOrderPagingRequest request)
        {
            var query = from p in _context.Orders
                        select new { p };
            if(request.Keyword!=0)
            {
                query = query.Where(x => x.p.Id.Equals(request.Keyword));
            }    


            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.pageIndex - 1) * request.pageSize)
                                    .Take(request.pageSize)
                                    .Select(x => new OrderViewModel()
                                    {
                                        Id=x.p.Id,
                                        Amount=x.p.Amount,
                                        Status=x.p.Status,
                                        Discount=x.p.Discount,
                                        Quantity=x.p.Quantity,
                                        TransactionId=x.p.TransactionId,
                                        CreatedDate=x.p.CreatedDate,
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

        public async Task<ApiResult<TransactionViewModel>> TransactionDetails(int transId)
        {
            var query = await _context.Transactions.FindAsync(transId);
            var result =  new TransactionViewModel()
            {
                Id = query.Id,
                AddressDistrict = query.AddressDistrict,
                AddressNumber = query.AddressNumber,
                AddressStreet = query.AddressStreet,
                Amount = query.Amount,
                ShippingFeeId = query.ShippingFeeId,
                Status = query.Status,
                CreatedDate = query.CreatedDate,
                Message = query.Message,
                PaymentMethod = query.PaymentMethod,
            };
            return new ApiSuccessResult<TransactionViewModel>(result);
                          
        }
    }
}
