using AutoMapper;
using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.Exceptions;

namespace ProductsManager.Application.Products.Commands.Update
{
    public class UpdateProductCommand : ProductCommandBase, IRequest<Unit>
    {
        public int Id { get; set; }       
    }

    public class UpateProductCommandHandler(IProductRepository productRepository,IMapper mapper) : IRequestHandler<UpdateProductCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await productRepository.GetByIdAsync(request.Id, cancellationToken) 
                ?? throw new ProductNotFoundException(request.Id);

            mapper.Map(request, existingProduct);
            await productRepository.UpdateAsync(existingProduct, cancellationToken);
            
            return Unit.Value;
        }
    }
}
