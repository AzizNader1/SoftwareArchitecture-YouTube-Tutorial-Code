using FluentValidation;

namespace ECommerce.Application.UseCases.Customer.Commands.UpdateCustomer
{
    public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Customer Id must be greater than 0.");
            RuleFor(x => x.Customer).NotNull().WithMessage("Customer cannot be null.");
            RuleFor(x => x.Customer.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.Customer.FirstName).MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
            RuleFor(x => x.Customer.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Customer.LastName).MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(x => x.Customer.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Customer.Email).EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Customer.Email).MaximumLength(150).WithMessage("Email cannot exceed 150 characters.");
            RuleFor(x => x.Customer.PhoneNumber).MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.");
        }
    }
}
