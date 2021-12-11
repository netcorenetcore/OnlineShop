using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Online.Shop.Business.Contracts;
using Online.Shop.Business.Data;
using Online.Shop.Business.Data.Entities;
using Online.Shop.Business.Service.Abstarctions;
using Online.Shop.Core.Data.Abstarctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Business.Service.Concretes
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork<ShopDbContext> unitOfWork;
        private readonly IMapper mapper;

        public StoreService(IUnitOfWork<ShopDbContext> unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<StoreDto> AddAsync(StoreDto dto, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Store>();
            var newEntity = mapper.Map<Store>(dto);
            newEntity = await repo.AddAsync(newEntity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<StoreDto>(newEntity);
        }

        public async  Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Store>();
            await repo.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);


        }

        public async Task<StoreDto> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Store>();
            var entity = await repo.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
            return mapper.Map<StoreDto>(entity);

        }

        public async Task<List<StoreDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Store>();
            var allEntity = await repo.ToListAsync(cancellationToken);
            return allEntity.Select(entity => mapper.Map<StoreDto>(entity)).ToList();
        }

        public async Task<StoreDto> UpdateAsync(StoreDto dto, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Store>();
            var entity = await repo.GetByIdAsync(dto.Id, cancellationToken);
            entity =  repo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<StoreDto>(entity);
        }
    }
}
