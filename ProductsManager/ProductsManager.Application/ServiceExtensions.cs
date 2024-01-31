using AutoMapper;
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

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
        }
    }
}
