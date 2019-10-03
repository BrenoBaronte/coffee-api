using CoffeeShop.Business;
using CoffeeShop.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeShop.Api.Modules
{
    public static class BusinessModule
    {
        public static IServiceCollection AddBusinessModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICoffeeServices, CoffeeServices>();

            return services;
        }
    }
}