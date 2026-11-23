using FluentValidation;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrdersByCustomerId
{
    public sealed class GetOrdersByCustomerIdQueryValidator : AbstractValidator<GetOrdersByCustomerIdQuery>
    {
        public GetOrdersByCustomerIdQueryValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Customer Id must be greater than 0.");
        }
    }
}
