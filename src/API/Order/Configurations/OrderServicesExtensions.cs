// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


using CocktailsApp.Application.Orders;
using CocktailsApp.Infrastructure.OrderAggregate;
using CocktailsApp.Infrastructure.SeedWork;

namespace CocktailsApp.API.Order
{
    public static class OrderServicesExtensions
    {
        public static IServiceCollection AddOrderRepositories(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }

        public static IServiceCollection AddOrderReaders(this IServiceCollection services)
        {
            ;
            services.AddScoped<IOrderReader, OrderReader>();
            return services;
        }

        public static IServiceCollection AddOrderProjectors(this IServiceCollection services)
        {
            services.AddScoped<IProjection, OrderProjection>();
            services.AddScoped<IProjection, OrderCrossAggregateProjection>();
            return services;
        }

        public static IServiceCollection AddOrderApplicationServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
