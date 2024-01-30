using ProductsManager.Application.Abstractions.Repositories;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Application.Abstractions
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
