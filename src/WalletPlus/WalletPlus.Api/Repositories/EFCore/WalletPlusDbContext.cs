using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Users;
using WalletPlus.Api.Models.Wallets;
using WalletPlus.Api.Models.Enums;
using WalletPlus.Api.Models.WalletTransaction;

namespace WalletPlus.Api.Repositories.EFCore
{
    public class WalletPlusDbContext : DbContext
    {
        public WalletPlusDbContext(DbContextOptions<WalletPlusDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WalletTransaction>().ToTable("Transactions");

            modelBuilder.Entity<Wallet>()
                        .Property(d => d.Type)
                        .HasConversion<string>(new EnumToStringConverter<WalletType>());
            modelBuilder.Entity<WalletTransaction>()
                        .Property(d => d.Type)
                        .HasConversion<string>(new EnumToStringConverter<WalletTransactionType>());
        }

        public override int SaveChanges()
        {
            var entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Added
                               || e.State == EntityState.Modified
                           select e.Entity;
            foreach (var entity in entities)
            {
                var validationContext = new ValidationContext(entity);
                Validator.ValidateObject(entity, validationContext);
            }
            return base.SaveChanges();
        }
    }
}
