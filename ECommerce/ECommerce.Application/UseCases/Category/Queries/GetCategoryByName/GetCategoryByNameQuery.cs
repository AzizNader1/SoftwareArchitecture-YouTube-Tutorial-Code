using ECommerce.Application.DTOs.CategoryDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetCategoryByName
{
    public sealed record GetCategoryByNameQuery(string Name) : IRequest<GetCategoryDto>
    {
    }
}
