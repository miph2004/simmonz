using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
       public ApiSuccessResult(T resultObj)
        {
            IsSucceed = true;
            ResultObject = resultObj;
        }
        public ApiSuccessResult()
        {
            IsSucceed = true;
        }
    }

    
}
