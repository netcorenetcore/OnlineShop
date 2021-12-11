using Microsoft.Extensions.DependencyInjection;
using Online.Shop.Business.Service.Abstarctions;
using Online.Shop.Business.Service.Concretes;
using Online.Shop.Business.Service.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Online.Shop.Business.Service.Extensions
{
    public static class ServiceExtenisons
    {
        public static IServiceCollection AddShopService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ShopProfile));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ISaleService, SaleService>();
            return services;
        }
    }
}
