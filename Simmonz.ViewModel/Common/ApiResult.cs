using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Common
{
    public class ApiResult<T>
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
        public T ResultObject { get; set; }
    }
}
