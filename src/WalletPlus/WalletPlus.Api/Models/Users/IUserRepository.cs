using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Common;

namespace WalletPlus.Api.Models.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string Email);
    }
}
