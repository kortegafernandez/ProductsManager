using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ProductsManager.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());              
            });
        }
    }
}
