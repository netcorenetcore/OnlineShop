using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Online.Shop.Business.Contracts;
using Online.Shop.Business.Service.Abstarctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Business.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;

        public ProductController(IProductService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public async Task<ProductDto> AddAsync(ProductDto dto, CancellationToken cancellationToken)
        {
            return await service.AddAsync(dto, cancellationToken);
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            await service.DeleteAsync(id,  cancellationToken);


        }

        [HttpGet("get/{id}")]
        public async Task<ProductDto> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await service.GetByIdAsync(id, cancellationToken);

        }

        [HttpGet("list")]
        public async Task<List<ProductDto>> GetListAsync(CancellationToken cancellationToken)
        {
            return await service.GetListAsync( cancellationToken);
        }

        [HttpPut("update")]
        public async Task<ProductDto> UpdateAsync(ProductDto dto, CancellationToken cancellationToken)
        {
            return await service.UpdateAsync(dto, cancellationToken);
        }
    }
}
