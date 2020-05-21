using System;
using System.Collections.Generic;
using System.Text;

namespace Simmonz.ViewModel.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string [] ValidationErrors { get; set; }
        public ApiErrorResult()
        {

        }
        public ApiErrorResult(string message)
        {
            IsSucceed = false;
            Message = message;
        }
        public ApiErrorResult(string[] validationErrors)
        {
            IsSucceed = false;
            ValidationErrors = validationErrors;
        }
    }
}
