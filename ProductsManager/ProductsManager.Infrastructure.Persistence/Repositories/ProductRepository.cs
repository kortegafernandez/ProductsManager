using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Infrastructure.Persistence.Repositories
{
    public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
    }
}
