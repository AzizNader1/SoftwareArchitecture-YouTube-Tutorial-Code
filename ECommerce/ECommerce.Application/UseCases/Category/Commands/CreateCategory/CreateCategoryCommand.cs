using ECommerce.Application.DTOs.CategoryDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(CreateCategoryDto Category) : IRequest<int>
    {
    }
}
