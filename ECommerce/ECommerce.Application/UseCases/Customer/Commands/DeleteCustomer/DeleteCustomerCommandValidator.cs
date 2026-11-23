using FluentValidation;

namespace ECommerce.Application.UseCases.Customer.Commands.DeleteCustomer
{
    public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Customer Id must be greater than 0.");
        }
    }
}
