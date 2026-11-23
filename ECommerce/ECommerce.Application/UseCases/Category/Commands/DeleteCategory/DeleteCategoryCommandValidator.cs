using FluentValidation;

namespace ECommerce.Application.UseCases.Category.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Category Id must be greater than 0.");
        }
    }
}
