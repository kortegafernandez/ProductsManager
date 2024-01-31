using FluentValidation;

namespace ProductsManager.Application.Products.Commands
{
    public abstract class ProductCommandBaseValidator<T> : AbstractValidator<T>
    where T : ProductCommandBase
    {
        public ProductCommandBaseValidator()
        {
            RuleFor(p => p.Name)
                .NotNull();

            RuleFor(p => p.StatusId)
                .Must(p =>
                    p switch
                    {
                        0 => true,
                        1 => true,
                        _ => false
                    }).WithMessage("Status must be 0 or 1.");

            RuleFor(p => p.Stock)
                .NotNull();

            RuleFor(p => p.Description)
                .NotNull();

            RuleFor(p => p.Price)
                .NotNull();
        }
    }
}
