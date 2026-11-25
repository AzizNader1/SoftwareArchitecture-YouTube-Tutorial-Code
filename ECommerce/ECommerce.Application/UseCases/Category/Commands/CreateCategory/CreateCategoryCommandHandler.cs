using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request through the CreateCategoryCommandValidator

            // 2. Map the request DTO to the domain model
            var category = new Domain.Entities.Category
            {
                Name = request.Category.Name,
                Description = request.Category.Description
            };

            // 3. Call the repository to create the category
            await _unitOfWork.CategoryWriteRepository.AddAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. Return the ID of the created category
            return category.Id;
        }
    }
}
