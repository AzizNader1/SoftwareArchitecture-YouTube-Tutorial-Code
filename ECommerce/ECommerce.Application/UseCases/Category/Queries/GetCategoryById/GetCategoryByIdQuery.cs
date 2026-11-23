using ECommerce.Application.DTOs.CategoryDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetCategoryById
{
    public sealed record GetCategoryByIdQuery(int Id) : IRequest<GetCategoryDto>
    {
    }
}
