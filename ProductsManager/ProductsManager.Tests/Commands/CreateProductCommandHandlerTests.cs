using AutoMapper;
using Moq;
using ProductsManager.Application.Abstractions;
using ProductsManager.Application.Mappings;
using ProductsManager.Application.Products.Commands.Create;
using ProductsManager.Domain.Entities;

namespace ProductsManager.Tests.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly IMapper mapper;
        public CreateProductCommandHandlerTests()
        {
            productRepositoryMock = new Mock<IProductRepository>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateProduct()
        {
            var command = new CreateProductCommand
            {
                Name = "name",
                Description = "description",                
            };           

            var handler = new CreateProductCommandHandler(
                productRepositoryMock.Object,
                mapper);

            var result = await handler.Handle(command, default);

            productRepositoryMock.Verify(p => 
                p.InsertAsync(It.Is<Product>(pr => pr.Name == "name" && pr.Description == "description")
                , It.IsAny<CancellationToken>()), Times.Once());            
        }
    }
}