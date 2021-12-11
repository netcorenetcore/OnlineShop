using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Online.Shop.Core.Data.Abstarctions;
using Online.Shop.Core.Data.Concretes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Online.Shop.Core.Data.Extensions
{
    public static class ServiceExtenisons
    {
        public static IServiceCollection AddDataAccessService<TDbContext>(this IServiceCollection services) where TDbContext:DbContext
        {
            services.AddDbContext<TDbContext>(conf => conf.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Sales3;integrated security=true;"));
            services.AddScoped<IUnitOfWork<TDbContext>, UnitOfWork<TDbContext>>();
            return services;
        }
    }
}
