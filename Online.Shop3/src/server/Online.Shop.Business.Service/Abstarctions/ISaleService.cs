using Online.Shop.Business.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Business.Service.Abstarctions
{
    public interface ISaleService
    {
        Task<SaleDto> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<List<SaleDto>> GetListAsync( CancellationToken cancellationToken);
        Task<SaleDto> AddAsync(SaleDto dto, CancellationToken cancellationToken);
        Task<SaleDto> UpdateAsync(SaleDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
