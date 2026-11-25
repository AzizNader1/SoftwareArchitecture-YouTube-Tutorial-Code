using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Common;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly IProductReadRepository _productReadRepository;

        public DeleteCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryReadRepository categoryReadRepository,
            IProductReadRepository productReadRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryReadRepository = categoryReadRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate that the category exists
            var category = await _categoryReadRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Category), request.Id);
            }

            // 2. Business rule: Prevent deletion if category still has linked products
            var hasProducts = await _productReadRepository.HasProductsByCategoryIdAsync(request.Id, cancellationToken);
            if (hasProducts)
            {
                throw new DomainException($"Cannot delete category '{category.Name}' (ID: {request.Id}) because it currently contains active products.");
            }

            // 3. Call the repository to delete the category
            await _unitOfWork.CategoryWriteRepository.DeleteAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. Return success status
            return true;
        }
    }
}
