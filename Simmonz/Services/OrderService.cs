using Simmonz.Data.EF;
using Simmonz.Data.Entities;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Services
{
    public class OrderService : IOrderService
    {
        private readonly SimmonzDbContext _context;
        public OrderService(SimmonzDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResult<bool>> Checkout(CheckOutRequest request)
        {
            var transaction = new Transaction()
            {
                AdminId = 1,
                ClientId = 1,
                ShippingFeeId = 1,
                Status = 0,
                CreatedDate=DateTime.Now,
            };
            var order = new Order()
            {
                CreatedDate = DateTime.Now,
                Status=0,
                Discount=0,
                TransactionId=transaction.Id,
            };
            return new ApiSuccessResult<bool>();
        }
    }
}
