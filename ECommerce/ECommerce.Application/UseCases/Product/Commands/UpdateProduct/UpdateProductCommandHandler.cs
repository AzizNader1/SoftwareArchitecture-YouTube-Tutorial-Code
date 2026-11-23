using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request (e.g., check for null values, validate data types, etc.) through the UpdateProductCommandValidator

            // 2. Map the request DTO to the domain model (if necessary)
            var product = new Domain.Entities.Product
            {
                Id = request.Id,
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                StockQuantity = request.Product.StockQuantity,
                CategoryId = request.Product.CategoryId
            };

            // 3. Call the repository to update the product
            _unitOfWork.ProductRepository.Update(request.Id, product);
            await _unitOfWork.SaveChangesAsync();

            // 4. Return success status
            return true;
        }
    }
}
