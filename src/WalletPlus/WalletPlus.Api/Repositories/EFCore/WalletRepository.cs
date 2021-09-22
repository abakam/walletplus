using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Wallets;

namespace WalletPlus.Api.Repositories.EFCore
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(WalletPlusDbContext context)
            :base(context)
        {

        }
    }
}
