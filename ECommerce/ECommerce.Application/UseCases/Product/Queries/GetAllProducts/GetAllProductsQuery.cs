using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.ProductDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Queries.GetAllProducts
{
    public sealed record GetAllProductsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<GetProductDto>>
    {
    }
}
