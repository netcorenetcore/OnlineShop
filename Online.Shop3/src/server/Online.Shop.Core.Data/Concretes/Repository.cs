using Microsoft.EntityFrameworkCore;
using Online.Shop.Core.Data.Abstarctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Core.Data.Concretes
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }

        public Type ElementType => _dbSet.AsQueryable().ElementType;

        public Expression Expression => _dbSet.AsQueryable().Expression;

        public IQueryProvider Provider => _dbSet.AsQueryable().Provider;

        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var entry = await _dbSet.AddAsync(entity, cancellationToken);
            return entry.Entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            Delete(entity);
        }

        public async Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _dbSet.AsQueryable().GetEnumerator();
        }

        public TEntity Update(TEntity entity)
        {
            var entry = _dbSet.Update(entity);
            return entry.Entity;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


    }
}
