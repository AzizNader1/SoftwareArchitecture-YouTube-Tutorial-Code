using ECommerce.Application.DTOs.ProductDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(int Id, UpdateProductDto Product) : IRequest<bool>
    {
    }
}
