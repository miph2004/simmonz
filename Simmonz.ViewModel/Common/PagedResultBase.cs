using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Common
{
    public class PagedResultBase
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int TotalRecords{get;set;}
        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / pageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
