namespace ProductsManager.Application.Abstractions.Clients
{
    public interface IDiscountAPIClient
    {
        Task<decimal> GetDiscountByProductIdAsync(int productId);
    }
}
