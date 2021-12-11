using Microsoft.EntityFrameworkCore;
using Online.Shop.Core.Data.Abstarctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Core.Data.Concretes
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<TEntity> GetRepositoy<TEntity>() where TEntity : EntityBase
        {
            var dbSet = _dbContext.Set<TEntity>();
            return new Repository<TEntity>(dbSet);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var entries = _dbContext.ChangeTracker.Entries().Where(e => e.Entity is EntityBase && (e.State == EntityState.Deleted));
            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                var entityBase = entry.Entity as EntityBase;
                entityBase.IsDeleted = true;
            }
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
