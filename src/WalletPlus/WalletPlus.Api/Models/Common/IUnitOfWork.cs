using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Users;
using WalletPlus.Api.Models.Wallets;
using WalletPlus.Api.Models.WalletTransaction;
using WalletPlus.Api.Repositories.EFCore;

namespace WalletPlus.Api.Models.Common
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IWalletRepository Wallets { get; }
        IWalletTransactionRepository WalletTransactions { get; }
        WalletPlusDbContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}
