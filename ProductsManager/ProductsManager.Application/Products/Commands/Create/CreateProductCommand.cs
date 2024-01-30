using MediatR;
using ProductsManager.Application.Abstractions;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Application.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<Unit>
    {
        public string Name { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand,Unit>
    {        
        public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new  Product { Name = request.Name, StatusId = request.StatusId, Stock = request.Stock, Price = request.Price };
            await productRepository.InsertAsync(product, cancellationToken);

            return Unit.Value;
        }
    }
}
