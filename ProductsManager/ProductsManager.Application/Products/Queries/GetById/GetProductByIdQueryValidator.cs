using FluentValidation;

namespace ProductsManager.Application.Products.Queries.GetById
{
    public class GetProductByIdQueryValidator:AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator() {
            RuleFor(p => p.Id)
               .NotEmpty();
        }
    }
}
