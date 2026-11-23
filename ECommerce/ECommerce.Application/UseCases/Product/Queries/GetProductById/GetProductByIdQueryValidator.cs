using FluentValidation;

namespace ECommerce.Application.UseCases.Product.Queries.GetProductById
{
    public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Product Id must be greater than 0.");
        }
    }
}
