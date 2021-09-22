using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.ViewModels
{
    public class ApiResult<T>
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
