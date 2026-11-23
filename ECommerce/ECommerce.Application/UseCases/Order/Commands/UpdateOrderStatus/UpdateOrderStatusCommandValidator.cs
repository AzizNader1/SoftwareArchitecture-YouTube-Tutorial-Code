using FluentValidation;

namespace ECommerce.Application.UseCases.Order.Commands.UpdateOrderStatus
{
    public sealed class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Order Id must be greater than 0.");
            RuleFor(x => x.StatusDto).NotNull().WithMessage("Status data cannot be null.");
            RuleFor(x => x.StatusDto.Status).IsInEnum().WithMessage("Invalid order status.");
        }
    }
}
