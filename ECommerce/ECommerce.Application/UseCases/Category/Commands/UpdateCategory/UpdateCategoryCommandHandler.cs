using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryReadRepository _categoryReadRepository;

        public UpdateCategoryCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryReadRepository categoryReadRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate that the category exists
            var categoryExists = await _categoryReadRepository.ExistsAsync(request.Id, cancellationToken);
            if (!categoryExists)
            {
                throw new NotFoundException(nameof(Domain.Entities.Category), request.Id);
            }

            // 2. Map the request DTO to the domain model
            var category = new Domain.Entities.Category
            {
                Id = request.Id,
                Name = request.Category.Name,
                Description = request.Category.Description
            };

            // 3. Call the repository to update the category
            await _unitOfWork.CategoryWriteRepository.UpdateAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. Return success status
            return true;
        }
    }
}
