using CoffeeShop.Domain.Abstractions;
using CoffeeShop.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Api.Modules
{
    public static class RepositoryModule
    {
        public static IServiceCollection AddRepositoryModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICoffeeRepository, CoffeeRepository>();

            return services;
        }
    }
}