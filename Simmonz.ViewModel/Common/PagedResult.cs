using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Common
{
    public class PagedResult<T> :PagedResultBase
    {
        public List<T> Items { set; get; }
    }
}
