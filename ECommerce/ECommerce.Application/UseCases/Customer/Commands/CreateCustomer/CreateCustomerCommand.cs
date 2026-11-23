using ECommerce.Application.DTOs.CustomerDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.CreateCustomer
{
    public sealed record CreateCustomerCommand(CreateCustomerDto Customer) : IRequest<int>
    {
    }
}
