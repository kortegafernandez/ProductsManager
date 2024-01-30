using AutoMapper;
using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.DTOs;

namespace ProductsManager.Application.Products.Queries.GetAll
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
    }

    public class GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper) : IRequestHandler<GetProductsQuery, IEnumerable<ProductDTO>>
    {
        public async Task<IEnumerable<ProductDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products =  await productRepository.GetAllAsync(cancellationToken);
            return mapper.Map<List<ProductDTO>>(products);
        }
    }
}
