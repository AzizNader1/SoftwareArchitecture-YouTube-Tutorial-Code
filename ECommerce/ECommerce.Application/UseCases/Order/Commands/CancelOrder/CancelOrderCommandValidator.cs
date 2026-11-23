using FluentValidation;

namespace ECommerce.Application.UseCases.Order.Commands.CancelOrder
{
    public sealed class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Order Id must be greater than 0.");
        }
    }
}
