using ECommerce.Application.DTOs.CustomerDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Queries.GetAllCustomers
{
    public sealed record GetAllCustomersQuery : IRequest<IEnumerable<GetCustomerDto>>
    {
    }
}
