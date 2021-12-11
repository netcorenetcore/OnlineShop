using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online.Shop.Business.Contracts;
using Online.Shop.Business.Data;
using Online.Shop.Business.Data.Entities;
using Online.Shop.Business.Service.Abstarctions;
using Online.Shop.Core.Data.Abstarctions;
using Online.Shop.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Business.Service.Concretes
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork<ShopDbContext> unitOfWork;
        private readonly IMapper mapper;
        private readonly IBarcodeService barcodeService;

        public SaleService(IUnitOfWork<ShopDbContext> unitOfWork,IMapper mapper,IBarcodeService barcodeService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.barcodeService = barcodeService;
        }
        public async Task<SaleDto> AddAsync(SaleDto dto, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Sale>();
            var strBarcodeService = await barcodeService.BarCodeSorgula(dto.ProductBarcodeNumber, cancellationToken);
            dto.Price = strBarcodeService.TFiyat;
            var newEntity = mapper.Map<Sale>(dto);
            newEntity = await repo.AddAsync(newEntity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<SaleDto>(newEntity);
        }

        public async  Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Sale>();
            await repo.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);


        }

        public async Task<SaleDto> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Sale>();
            var entity = await repo.Include(x=>x.Product).Include(x => x.Store).Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
            return mapper.Map<SaleDto>(entity);

        }

        public async Task<List<SaleDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Sale>();
            var allEntity = await repo.Include(x => x.Product).Include(x => x.Store).ToListAsync(cancellationToken);
            return allEntity.Select(entity => mapper.Map<SaleDto>(entity)).ToList();
        }

        public async Task<SaleDto> UpdateAsync(SaleDto dto, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Sale>();
            var entity = await repo.GetByIdAsync(dto.Id, cancellationToken);
            entity =  repo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<SaleDto>(entity);
        }
    }
}
