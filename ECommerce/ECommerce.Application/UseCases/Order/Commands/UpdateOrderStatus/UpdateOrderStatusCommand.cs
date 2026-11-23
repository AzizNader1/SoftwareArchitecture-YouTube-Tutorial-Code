using ECommerce.Application.DTOs.OrderDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.UpdateOrderStatus
{
    public sealed record UpdateOrderStatusCommand(int Id, UpdateOrderStatusDto StatusDto) : IRequest<bool>
    {
    }
}
