using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WalletPlus.Api.Models.Common;

namespace WalletPlus.Api.Repositories.EFCore
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected WalletPlusDbContext _context;
        internal DbSet<T> table;
        private readonly ILogger _logger;
        
        public Repository(WalletPlusDbContext context, ILogger logger)
        {
            _context = context;
            table = context.Set<T>();
            _logger = logger;
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await table.ToListAsync();
        }
        public virtual async Task<T> GetById(string id)
        {
            return await table.FindAsync(id);
        }
        public virtual async Task<bool> Add(T entity)
        {
            await table.AddAsync(entity);
            return true;
        }
        public virtual async Task<bool> Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return true;
        }
        public virtual async Task<bool> Delete(string id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
            return true;
        }
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await table.Where(predicate).ToListAsync();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
