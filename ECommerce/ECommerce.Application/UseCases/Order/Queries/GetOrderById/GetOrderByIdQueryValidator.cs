using FluentValidation;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrderById
{
    public sealed class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
    {
        public GetOrderByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Order Id must be greater than 0.");
        }
    }
}
