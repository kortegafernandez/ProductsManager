using AutoMapper;
using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Application.Abstractions.Clients;
using ProductsManager.Domain.DTOs;
using ProductsManager.Domain.Exceptions;

namespace ProductsManager.Application.Products.Queries.GetById
{
    public  class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int Id { get; set; } 
    }

    public class GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper,IDiscountAPIClient discountAPIClient) : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new ProductNotFoundException(request.Id);

            var productDTO = mapper.Map<ProductDTO>(product);
            decimal discountResponse = await discountAPIClient.GetDiscountByProductIdAsync(request.Id);
           
            productDTO.Discount = discountResponse;
            productDTO.FinalPrice = productDTO.Price * (100 - discountResponse) / 100;
            return productDTO;
        }
    }
}
