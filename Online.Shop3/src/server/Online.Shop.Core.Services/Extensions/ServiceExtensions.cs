using Microsoft.Extensions.DependencyInjection;
using Nest;
using Online.Shop.Core.Services.Abstractions;
using Online.Shop.Core.Services.Concretes;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Online.Shop.Core.Services.Extensions
{
    public static class ServiceExtenisons
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddDistributedRedisCache(conf =>
            {
                conf.Configuration = "configure";
            });

            var connectionFactory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                AutomaticRecoveryEnabled = true,
                HostName = "localhost",
                Port = 5672,
                DispatchConsumersAsync = true
            };

            var connection = connectionFactory.CreateConnection();
            services.AddSingleton(connection);

            services.AddScoped(serviceProvider =>
            {
                var conn = serviceProvider.GetService<IConnection>();
                return conn.CreateModel();
            });

            services.AddHttpClient<IBarcodeService, BarcodeService>(conf =>
            {
                conf.BaseAddress = new Uri("");
            });

            // ADD ELASTIC CLIENT
            services.AddSingleton<IConnectionSettingsValues, ConnectionSettings>(p =>
            {
                return new ConnectionSettings(new Uri("http://localhost:9200"));
                // .BasicAuthentication("elasticUserName", "elasticPassword")
                //.ServerCertificateValidationCallback((sender, cert, chain, errors) => true);
            });
            services.AddScoped<IElasticClient, ElasticClient>();

            return services;
        }
    }
}
