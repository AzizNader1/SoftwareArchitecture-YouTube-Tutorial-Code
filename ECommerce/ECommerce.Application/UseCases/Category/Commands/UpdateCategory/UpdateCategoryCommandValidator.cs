using FluentValidation;

namespace ECommerce.Application.UseCases.Category.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Category Id must be greater than 0.");
            RuleFor(x => x.Category).NotNull().WithMessage("Category cannot be null.");
            RuleFor(x => x.Category.Name).NotEmpty().WithMessage("Category name is required.");
            RuleFor(x => x.Category.Name).MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");
            RuleFor(x => x.Category.Description).MaximumLength(500).WithMessage("Category description cannot exceed 500 characters.");
        }
    }
}
