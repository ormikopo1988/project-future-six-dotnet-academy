using DI.TinyCrm.Core.Interfaces;
using DI.TinyCrm.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DI.TinyCrm.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
