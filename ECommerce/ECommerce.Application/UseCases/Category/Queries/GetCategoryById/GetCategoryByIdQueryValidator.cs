using FluentValidation;

namespace ECommerce.Application.UseCases.Category.Queries.GetCategoryById
{
    public sealed class GetCategoryByIdQueryValidator : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Category Id must be greater than 0.");
        }
    }
}
