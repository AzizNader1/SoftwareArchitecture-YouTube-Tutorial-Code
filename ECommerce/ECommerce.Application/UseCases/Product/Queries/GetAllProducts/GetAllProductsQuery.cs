using ECommerce.Application.DTOs.ProductDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery : IRequest<IEnumerable<GetProductDto>>
    {
    }
}
