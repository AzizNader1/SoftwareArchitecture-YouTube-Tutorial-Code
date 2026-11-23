using ECommerce.Application.DTOs.CustomerDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Queries.GetCustomerById
{
    public sealed record GetCustomerByIdQuery(int Id) : IRequest<GetCustomerDto>
    {
    }
}
