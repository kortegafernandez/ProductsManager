using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Application.Products.Queries.GetAll
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }

    public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.GetAllAsync(cancellationToken);
        }
    }
}
