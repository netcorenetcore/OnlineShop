using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Core.Data.Abstarctions
{
    public interface IRepository<TEntity> : IQueryable<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
