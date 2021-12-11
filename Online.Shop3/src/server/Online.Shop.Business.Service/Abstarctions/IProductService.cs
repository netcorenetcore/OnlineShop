using Online.Shop.Business.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Business.Service.Abstarctions
{
    public interface IProductService
    {
        Task<ProductDto> GetByIdAsync(long id, CancellationToken cancellationToken);
        Task<List<ProductDto>> GetListAsync( CancellationToken cancellationToken);
        Task<ProductDto> AddAsync(ProductDto dto, CancellationToken cancellationToken);
        Task<ProductDto> UpdateAsync(ProductDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(long id, CancellationToken cancellationToken);
    }
}
