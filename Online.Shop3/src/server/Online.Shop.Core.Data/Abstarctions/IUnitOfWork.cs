using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Core.Data.Abstarctions
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        IRepository<TEntity> GetRepositoy<TEntity>() where TEntity : EntityBase;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
