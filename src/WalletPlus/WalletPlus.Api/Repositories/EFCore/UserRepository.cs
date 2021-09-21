using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Users;

namespace WalletPlus.Api.Repositories.EFCore
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(WalletPlusDbContext context, ILogger logger) 
            : base(context, logger)
        {

        }
    }
}
