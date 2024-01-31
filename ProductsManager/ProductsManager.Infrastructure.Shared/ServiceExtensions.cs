using Microsoft.Extensions.DependencyInjection;
using ProductsManager.Application.Abstractions.Clients;
using ProductsManager.Infrastructure.Shared.Clients;

namespace ProductsManager.Infrastructure.Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddHttpClient<IDiscountAPIClient, DiscountAPIClient>()
                .ConfigureHttpClient((serviceProvider, httpClient) =>
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.BaseAddress = new Uri("https://65b962a1b71048505a8aca9c.mockapi.io/api/");
                }
            );
        }
    }
}
