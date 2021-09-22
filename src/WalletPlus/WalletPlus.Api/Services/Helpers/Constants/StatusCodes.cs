using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Services.Helpers.Constants
{
    public static class StatusCodes
    {
        public const string UNAUTHORISED_USER = "401";
        public const string INTERNAL_ERROR = "500";
        public const string ALREADY_EXISTS = "422";
        public const string RECORD_DO_NOT_EXISTS = "404";
    }
}
