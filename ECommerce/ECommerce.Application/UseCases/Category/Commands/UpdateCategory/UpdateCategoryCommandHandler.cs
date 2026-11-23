using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request through the UpdateCategoryCommandValidator

            // 2. Map the request DTO to the domain model
            var category = new Domain.Entities.Category
            {
                Id = request.Id,
                Name = request.Category.Name,
                Description = request.Category.Description
            };

            // 3. Call the repository to update the category
            _unitOfWork.CategoryRepository.Update(request.Id, category);
            await _unitOfWork.SaveChangesAsync();

            // 4. Return success status
            return true;
        }
    }
}
