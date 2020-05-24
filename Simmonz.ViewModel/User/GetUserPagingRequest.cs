using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.User
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public List<int> CategoryId { get; set; }
    }
}
