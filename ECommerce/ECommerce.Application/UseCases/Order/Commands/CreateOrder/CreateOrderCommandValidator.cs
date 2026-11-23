using FluentValidation;

namespace ECommerce.Application.UseCases.Order.Commands.CreateOrder
{
    public sealed class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order).NotNull().WithMessage("Order cannot be null.");
            RuleFor(x => x.Order.CustomerId).GreaterThan(0).WithMessage("Valid customer ID is required.");
            RuleFor(x => x.Order.Items).NotEmpty().WithMessage("Order must contain at least one item.");

            RuleForEach(x => x.Order.Items).ChildRules(item =>
            {
                item.RuleFor(i => i.ProductId).GreaterThan(0).WithMessage("Valid product ID is required.");
                item.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Item quantity must be greater than zero.");
                item.RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("Item unit price must be greater than zero.");
            });
        }
    }
}
