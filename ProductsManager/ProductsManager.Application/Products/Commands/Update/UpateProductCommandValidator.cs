using FluentValidation;

namespace ProductsManager.Application.Products.Commands.Update
{
    public class UpateProductCommandValidator : ProductCommandBaseValidator<UpdateProductCommand>
    {
        public UpateProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty();
        }
    }
}
