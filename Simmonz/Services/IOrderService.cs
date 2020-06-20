using Simmonz.Data.Entities;
using Simmonz.ViewModel.Common;
using Simmonz.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.Services
{
    public  interface IOrderService 
    {
        Task<ApiResult<bool>> Checkout(CheckOutRequest request);
    }
}
