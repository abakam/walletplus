using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Users;

namespace WalletPlus.Api.Services.Helpers
{
    public interface ITokenHelper
    {
        string GenerateSecureSecret();
        string GenerateToken(User user);
    }
}
