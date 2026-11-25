using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.CategoryDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetAllCategories
{
    public sealed record GetAllCategoriesQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<GetCategoryDto>>
    {
    }
}
