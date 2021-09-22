using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using WalletPlus.Api.Models.Users;
using WalletPlus.Api.Models.Wallets;
using WalletPlus.Api.Models.Common;
using WalletPlus.Api.Models.WalletTransaction;

namespace WalletPlus.Api.Repositories.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WalletPlusDbContext _context;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private IDbContextTransaction _entityTransaction;
        public IUserRepository Users { get; }

        public IWalletRepository Wallets { get; }
        public IWalletTransactionRepository WalletTransactions { get; }

        public UnitOfWork(WalletPlusDbContext context,
            IUserRepository userRepository,
            IWalletRepository walletRepository,
            IWalletTransactionRepository walletTransactionRepository)
        {
            _context = context;
            Users = userRepository;
            Wallets = walletRepository;
            WalletTransactions = walletTransactionRepository;
        }

        public WalletPlusDbContext Context
        {
            get { return _context; }
        }

        public void CreateTransaction()
        {
            _entityTransaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _entityTransaction.Commit();
        }

        public void Rollback()
        {
            _entityTransaction.Rollback();
            _entityTransaction.Dispose();
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (ValidationException dbEx)
            {
                foreach (var validationError in dbEx.ValidationResult.MemberNames)
                    _errorMessage += string.Format("Property: {0} Error: {1}", validationError, dbEx.Message) + Environment.NewLine;

                throw new Exception(_errorMessage, dbEx);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }
    }
}
