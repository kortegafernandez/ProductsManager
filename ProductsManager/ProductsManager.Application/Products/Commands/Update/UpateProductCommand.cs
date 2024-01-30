using MediatR;
using ProductsManager.Application.Abstractions;

namespace ProductsManager.Application.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }

    public class UpateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await productRepository.GetByIdAsync(request.Id, cancellationToken);
            existingProduct.Name = request.Name;
            existingProduct.Price = request.Price;
            existingProduct.StatusId = request.StatusId;
            existingProduct.Stock = request.Stock;
            existingProduct.Description = request.Description;

            await productRepository.UpdateAsync(existingProduct, cancellationToken);

            return Unit.Value;
        }
    }
}
