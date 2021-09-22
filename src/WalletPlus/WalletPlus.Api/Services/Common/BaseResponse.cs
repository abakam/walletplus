using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Services.Common
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string DisplayMessage { get; set; } 
        public string ErrorCode { get; set; }

    }

    public class BaseResponse<T> : BaseResponse
    {
        public T ReturnValue { get; set; }

        public static BaseResponse<T> WithSuccess(T value)
        {
            return new BaseResponse<T> { Success = true, ReturnValue = value };
        }

        public static BaseResponse<T> WithError(string displayMessage, string errorCode)
        {
            return new BaseResponse<T>
            {
                Success = false,
                DisplayMessage = displayMessage,
                ErrorCode = errorCode
            };
        }
    }
}
