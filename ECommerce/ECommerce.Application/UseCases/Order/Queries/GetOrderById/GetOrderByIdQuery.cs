using ECommerce.Application.DTOs.OrderDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrderById
{
    public sealed record GetOrderByIdQuery(int Id) : IRequest<GetOrderDto>
    {
    }
}
