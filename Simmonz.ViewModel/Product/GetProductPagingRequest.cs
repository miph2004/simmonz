using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Product
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
