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
    public class SaleController : ControllerBase
    {
        private readonly ISaleService service;

        public SaleController(ISaleService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public async Task<SaleDto> AddAsync(SaleDto dto, CancellationToken cancellationToken)
        {
            return await service.AddAsync(dto, cancellationToken);
        }

        [HttpDelete("delete/{id}")]
        public async Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            await service.DeleteAsync(id,  cancellationToken);


        }

        [HttpGet("get/{id}")]
        public async Task<SaleDto> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            return await service.GetByIdAsync(id, cancellationToken);

        }

        [HttpGet("list")]
        public async Task<List<SaleDto>> GetListAsync(CancellationToken cancellationToken)
        {
            return await service.GetListAsync( cancellationToken);
        }

        [HttpPut("update")]
        public async Task<SaleDto> UpdateAsync(SaleDto dto, CancellationToken cancellationToken)
        {
            return await service.UpdateAsync(dto, cancellationToken);
        }
    }
}
