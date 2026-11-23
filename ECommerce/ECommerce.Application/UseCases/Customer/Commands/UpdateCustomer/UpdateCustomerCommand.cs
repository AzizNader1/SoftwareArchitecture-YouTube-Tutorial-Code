using ECommerce.Application.DTOs.CustomerDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.UpdateCustomer
{
    public sealed record UpdateCustomerCommand(int Id, UpdateCustomerDto Customer) : IRequest<bool>
    {
    }
}
