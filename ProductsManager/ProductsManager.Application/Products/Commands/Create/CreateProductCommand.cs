using AutoMapper;
using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Application.Products.Commands.Create
{
    public class CreateProductCommand : ProductCommandBase, IRequest<Unit>
    {       
    }

    public class CreateProductCommandHandler(IProductRepository productRepository,IMapper mapper) : IRequestHandler<CreateProductCommand,Unit>
    {        
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = mapper.Map<Product>(request);
            await productRepository.InsertAsync(product, cancellationToken);

            return Unit.Value;
        }
    }
}
