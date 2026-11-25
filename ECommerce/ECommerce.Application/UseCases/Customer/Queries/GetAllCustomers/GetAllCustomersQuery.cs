using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.CustomerDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Queries.GetAllCustomers
{
    public sealed record GetAllCustomersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<GetCustomerDto>>
    {
    }
}
