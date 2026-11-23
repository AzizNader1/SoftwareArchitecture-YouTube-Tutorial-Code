using ECommerce.Application.DTOs.OrderDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrdersByCustomerId
{
    public sealed record GetOrdersByCustomerIdQuery(int CustomerId) : IRequest<IEnumerable<GetOrderDto>>
    {
    }
}
