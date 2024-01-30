using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsManager.Application.Abstractions;
using ProductsManager.Infrastructure.Persistence.Repositories;
using System;

namespace ProductsManager.Infrastructure.Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("SqliteConnection");
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ApplicationDbContextInitializer>();            
        }        
    }
}
