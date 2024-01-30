using ProductsManager.Application.Abstractions.Clients;
using ProductsManager.Domain.DTOs.DiscountAPI;
using System.Text.Json;

namespace ProductsManager.Infrastructure.Shared.Clients
{
    public class DiscountAPIClient(HttpClient client) : IDiscountAPIClient
    {
        public async Task<decimal> GetDiscountByProductIdAsync(int productId)
        {
            var descount = 0m;
                        
            var response = await client.GetAsync($"v1/products/{productId}");

            if (!response.IsSuccessStatusCode)
            {
                return descount;
            }

            var descountProduct = await JsonSerializer.DeserializeAsync<DiscountResponseDTO>(response.Content.ReadAsStream());

            descount = descountProduct?.discount ?? 0m;

            return descount;
        }
    }
}
