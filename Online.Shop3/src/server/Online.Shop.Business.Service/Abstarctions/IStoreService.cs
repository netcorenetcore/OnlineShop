using Online.Shop.Business.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Business.Service.Abstarctions
{
    public interface IStoreService
    {
        Task<StoreDto> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<List<StoreDto>> GetListAsync( CancellationToken cancellationToken);
        Task<StoreDto> AddAsync(StoreDto dto, CancellationToken cancellationToken);
        Task<StoreDto> UpdateAsync(StoreDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
