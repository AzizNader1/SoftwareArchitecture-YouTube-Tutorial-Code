using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.DeleteCustomer
{
    public sealed record DeleteCustomerCommand(int Id) : IRequest<bool>
    {
    }
}
