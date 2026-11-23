using FluentValidation;

namespace ECommerce.Application.UseCases.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Product Id must be greater than 0.");
        }
    }
}
