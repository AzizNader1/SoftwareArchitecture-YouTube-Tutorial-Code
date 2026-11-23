using FluentValidation;

namespace ECommerce.Application.UseCases.Customer.Queries.GetCustomerById
{
    public sealed class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Customer Id must be greater than 0.");
        }
    }
}
