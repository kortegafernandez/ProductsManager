using AutoMapper;
using Moq;
using ProductsManager.Application.Abstractions;
using ProductsManager.Application.Abstractions.Clients;
using ProductsManager.Application.Abstractions.MemoryCache;
using ProductsManager.Application.Constants;
using ProductsManager.Application.Mappings;
using ProductsManager.Application.Products.Queries.GetById;
using ProductsManager.Domain.Entities;
using ProductsManager.Domain.Exceptions;

namespace ProductsManager.Tests.Queries
{
    public class GetProductByIdQueryTests
    {
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly IMapper mapper;
        public GetProductByIdQueryTests()
        {
            productRepositoryMock = new Mock<IProductRepository>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ProductExists_ReturnsProductDTOWithDiscountAndStatusDescription()
        {
            // Arrange
            var productId = 1;
            var getProductByIdQuery = new GetProductByIdQuery { Id = productId };
            var cancellationToken = new CancellationToken();
                        
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId, cancellationToken))
                .ReturnsAsync(new Product { Id = productId, Price = 800, StatusId = 1 });
            
            var mockDiscountAPIClient = new Mock<IDiscountAPIClient>();
            mockDiscountAPIClient.Setup(client => client.GetDiscountByProductIdAsync(productId))
                .ReturnsAsync(50m); 

            var mockProductMemoryCache = new Mock<IProductMemoryCache>();
            mockProductMemoryCache.Setup(cache => cache.GetData<Dictionary<int, string>>(Constants.PRODUCT_CACHE_STATUS_KEY))
                .Returns(new Dictionary<int, string> { { 1, "Active" } }); 
                       
            var handler = new GetProductByIdQueryHandler(productRepositoryMock.Object, mapper, mockDiscountAPIClient.Object, mockProductMemoryCache.Object);

            // Act
            var result = await handler.Handle(getProductByIdQuery, cancellationToken);

            // Assert
            Assert.NotNull(result);          
            Assert.Equal(50,result.Discount);
            Assert.Equal(400, result.FinalPrice);            
            Assert.Equal("Active", result.StatusDescription);
        }

        [Fact]
        public async Task Handle_NonExistingProduct_ThrowsProductNotFoundException()
        {
            // Arrange
            var productId = 1;
            var getProductByIdQuery = new GetProductByIdQuery { Id = productId };
            var cancellationToken = new CancellationToken();
                       
            productRepositoryMock.Setup(repo => repo.GetByIdAsync(productId, cancellationToken))
                .ReturnsAsync((Product)null);
                       
            var mockDiscountAPIClient = new Mock<IDiscountAPIClient>();
            var mockProductMemoryCache = new Mock<IProductMemoryCache>();

            // Creating the handler with mocked dependencies
            var handler = new GetProductByIdQueryHandler(productRepositoryMock.Object, mapper, mockDiscountAPIClient.Object, mockProductMemoryCache.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(getProductByIdQuery, cancellationToken));
        }
    }
}
