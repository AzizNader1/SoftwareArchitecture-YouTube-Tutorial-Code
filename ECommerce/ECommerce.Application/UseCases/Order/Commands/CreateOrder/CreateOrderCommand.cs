using ECommerce.Application.DTOs.OrderDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.CreateOrder
{
    public sealed record CreateOrderCommand(CreateOrderDto Order) : IRequest<int>
    {
    }
}
