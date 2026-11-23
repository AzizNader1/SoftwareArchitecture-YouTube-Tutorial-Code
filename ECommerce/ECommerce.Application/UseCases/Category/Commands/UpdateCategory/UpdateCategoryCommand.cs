using ECommerce.Application.DTOs.CategoryDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.UpdateCategory
{
    public sealed record UpdateCategoryCommand(int Id, UpdateCategoryDto Category) : IRequest<bool>
    {
    }
}
