using ECommerce.Application.DTOs.CategoryDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery : IRequest<IEnumerable<GetCategoryDto>>
    {
    }
}
