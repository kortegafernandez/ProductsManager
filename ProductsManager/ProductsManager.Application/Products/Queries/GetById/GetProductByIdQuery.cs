using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Application.Products.Queries.GetById
{
    public  class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; } 
    }

    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product>
    {
        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await productRepository.GetByIdAsync(request.Id, cancellationToken);
        }
    }
}
