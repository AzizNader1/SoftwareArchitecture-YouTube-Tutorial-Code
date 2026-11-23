using ECommerce.Application.DTOs.OrderDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetAllOrders
{
    public sealed record GetAllOrdersQuery : IRequest<IEnumerable<GetOrderDto>>
    {
    }
}
