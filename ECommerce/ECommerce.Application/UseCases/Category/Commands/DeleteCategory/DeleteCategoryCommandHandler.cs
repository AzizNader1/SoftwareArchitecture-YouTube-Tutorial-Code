using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. Call the repository to delete the category
            _unitOfWork.CategoryRepository.Delete(request.Id);
            await _unitOfWork.SaveChangesAsync();

            // 2. Return success status
            return true;
        }
    }
}
