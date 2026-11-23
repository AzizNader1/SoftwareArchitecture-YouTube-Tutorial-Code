using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.DeleteProduct
{
    public sealed record DeleteProductCommand(int Id) : IRequest<bool>
    {
    }
}
