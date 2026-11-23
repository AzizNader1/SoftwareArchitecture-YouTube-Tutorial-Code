using ECommerce.Application.DTOs.ProductDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.CreateProduct
{
    public sealed record CreateProductCommand(CreateProductDto Product) : IRequest<int>
    {
    }
}
