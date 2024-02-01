using ProductsManager.Application.Products.Commands.Create;

namespace ProductsManager.Tests.Validators
{
    public class CreateProductCommandValidatorTests
    {
        [Fact]
        public void Validate_WhenMandatoryFieldIsEmpty_ShouldReturnError()
        {
            //Arrange
            var validator = new CreateProductCommandValidator();
            var command = new CreateProductCommand() { StatusId = 3};

            //Act

            var result = validator.Validate(command);

            //Assert

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.Name));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.StatusId));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.Stock));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.Description));
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(command.Price));

        }
    }
}
