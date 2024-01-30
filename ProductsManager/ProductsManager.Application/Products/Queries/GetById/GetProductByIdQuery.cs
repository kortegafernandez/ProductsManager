using AutoMapper;
using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.DTOs;

namespace ProductsManager.Application.Products.Queries.GetById
{
    public  class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int Id { get; set; } 
    }

    public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
            return mapper.Map<ProductDTO>(product);
        }
    }
}
