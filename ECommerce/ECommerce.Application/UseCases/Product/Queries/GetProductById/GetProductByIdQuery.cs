using ECommerce.Application.DTOs.ProductDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Queries.GetProductById
{
    public sealed record GetProductByIdQuery(int Id) : IRequest<GetProductDto>
    {
    }
}
