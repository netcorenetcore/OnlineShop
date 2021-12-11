using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nest;
using Online.Shop.Business.Contracts;
using Online.Shop.Business.Data;
using Online.Shop.Business.Data.Entities;
using Online.Shop.Business.Service.Abstarctions;
using Online.Shop.Core.Data.Abstarctions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Business.Service.Concretes
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork<ShopDbContext> unitOfWork;
        private readonly IMapper mapper;
        private readonly IModel channel;
        private readonly IElasticClient elasticClient;

        public ProductService(IUnitOfWork<ShopDbContext> unitOfWork,IMapper mapper,IModel channel,IElasticClient elasticClient)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.channel = channel;
            this.elasticClient = elasticClient;
        }
        public async Task<ProductDto> AddAsync(ProductDto dto, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Product>();
            var newEntity = mapper.Map<Product>(dto);
            newEntity = await repo.AddAsync(newEntity, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            dto = mapper.Map<ProductDto>(newEntity);
            channel.QueueDeclare("shopprice", true, false, false, null);
            channel.BasicPublish("", "shopprice", false, null, Encoding.UTF8.GetBytes(" test message"));
            await elasticClient.IndexAsync(dto, declare => declare.Index("Product"),cancellationToken);
            return dto;
        }

        public async  Task DeleteAsync(long id, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Product>();
            await repo.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);


        }

        public async Task<ProductDto> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Product>();
            var entity = await repo.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
            return mapper.Map<ProductDto>(entity);

        }

        public async Task<List<ProductDto>> GetListAsync(CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Product>();
            var allEntity = await repo.ToListAsync(cancellationToken);
            return allEntity.Select(entity => mapper.Map<ProductDto>(entity)).ToList();
        }

        public async Task<ProductDto> UpdateAsync(ProductDto dto, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepositoy<Product>();
            var entity = await repo.GetByIdAsync(dto.Id, cancellationToken);
            entity =  repo.Update(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return mapper.Map<ProductDto>(entity);
        }
    }
}
