using AutoMapper;
using Moq;
using ProductsManager.Application.Abstractions;
using ProductsManager.Application.Mappings;
using ProductsManager.Application.Products.Commands.Update;
using ProductsManager.Domain.Entities;
using ProductsManager.Domain.Exceptions;

namespace ProductsManager.Tests.Commands
{
    public class UpateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly IMapper mapper;
        public UpateProductCommandHandlerTest()
        {
            productRepositoryMock = new Mock<IProductRepository>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidProduct_UpdateProductInRepository()
        {
            // Arrange
            var productId = 1;
            var updateProductCommand = new UpdateProductCommand { Id = productId };
            var cancellationToken = new CancellationToken();

            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId, cancellationToken))
                .ReturnsAsync(new Product { Id = productId }); 

            var handler = new UpateProductCommandHandler(productRepositoryMock.Object, mapper);

            // Act
            await handler.Handle(updateProductCommand, cancellationToken);

            // Assert
            productRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>(), cancellationToken), Times.Once);
        }

        [Fact]
        public async Task Handle_NonExistingProduct_ThrowProductNotFoundException()
        {
            // Arrange
            var productId = 1;
            var updateProductCommand = new UpdateProductCommand { Id = productId, /* other properties */ };
            var cancellationToken = new CancellationToken();
                       
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId, cancellationToken))
                .ReturnsAsync((Product)null); 
            
            var handler = new UpateProductCommandHandler(productRepositoryMock.Object, mapper);

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(updateProductCommand, cancellationToken));
        }
    }
}
