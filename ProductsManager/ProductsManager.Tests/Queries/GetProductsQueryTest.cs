using AutoMapper;
using Moq;
using ProductsManager.Application.Abstractions;
using ProductsManager.Application.Mappings;
using ProductsManager.Application.Products.Queries.GetAll;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Tests.Queries
{
    public class GetProductsQueryTest
    {
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly IMapper mapper;

        public GetProductsQueryTest()
        {
            productRepositoryMock = new Mock<IProductRepository>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ReturnsMappedProductDTOs()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            productRepositoryMock.Setup(repo => repo.GetAllAsync(cancellationToken))
                .ReturnsAsync(new List<Product> { new Product { Id = 1, Name = "Product 1" }, new Product { Id = 2, Name = "Product 2" } });
            
            var handler = new GetProductsQueryHandler(productRepositoryMock.Object, mapper);

            // Act
            var result = await handler.Handle(new GetProductsQuery(), cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() == 2);
            Assert.True(result.First().Id == 1);
            Assert.True(result.Last().Id == 2);            
        }

        [Fact]
        public async Task Handle_EmptyProductList_ReturnsEmptyList()
        {
            // Arrange
            var cancellationToken = new CancellationToken();

            productRepositoryMock.Setup(repo => repo.GetAllAsync(cancellationToken))
                .ReturnsAsync(new List<Product>());
                        
            var handler = new GetProductsQueryHandler(productRepositoryMock.Object, mapper);

            // Act
            var result = await handler.Handle(new GetProductsQuery(), cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(!result.Any());
        }
    }
}
