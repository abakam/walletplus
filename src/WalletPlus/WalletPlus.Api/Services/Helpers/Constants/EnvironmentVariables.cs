using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WalletPlus.Api.Services.Helpers.Constants
{
    public class EnvironmentVariables
    {
        public static string JWT_SECRETKEY => Environment.GetEnvironmentVariable("WALLETPLUS_JWT_SECRETKEY");
        public static string JWT_ISSUER => Environment.GetEnvironmentVariable("WALLETPLUS_JWT_ISSUER");
        public static string JWT_AUDIENCE => Environment.GetEnvironmentVariable("WALLETPLUS_JWT_AUDIENCE");
    }
}
