using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.DeleteCategory
{
    public sealed record DeleteCategoryCommand(int Id) : IRequest<bool>
    {
    }
}
