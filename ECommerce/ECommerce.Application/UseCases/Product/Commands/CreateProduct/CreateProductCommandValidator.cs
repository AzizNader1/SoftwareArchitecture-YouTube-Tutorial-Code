using FluentValidation;

namespace ECommerce.Application.UseCases.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Product).NotNull().WithMessage("Product cannot be null.");
            RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Product description is required.");
            RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Product price must be greater than zero.");
            RuleFor(x => x.Product.StockQuantity).GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");
            RuleFor(x => x.Product.CategoryId).GreaterThan(0).WithMessage("Valid Category ID is required.");
        }
    }
}
