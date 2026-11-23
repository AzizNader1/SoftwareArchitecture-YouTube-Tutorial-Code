using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.CancelOrder
{
    public sealed record CancelOrderCommand(int Id) : IRequest<bool>
    {
    }
}
