using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Order
{
    public class GetOrderPagingRequest : PagingRequestBase
    {
        public int Keyword { get; set; }
    }
}
