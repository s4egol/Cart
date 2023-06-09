﻿using Cart.Business.Implementations;
using Cart.Business.Interfaces;
using Cart.DataAccess.Interfaces;
using Cart.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NoSql.Context;

namespace DependencyResolver
{
    public static class ConfigResolver
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICartingService, CartingService>();

            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();

            return services;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, string connectionString)
        {
            var dbContext = new CartContext(connectionString);
            services.AddSingleton(dbContext);

            return services;
        }
    }
}