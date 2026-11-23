using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request (e.g., check for null values, validate data types, etc.) through the CreateProductCommandValidator

            // 2. Map the request DTO to the domain model (if necessary)
            var product = new Domain.Entities.Product
            {
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                StockQuantity = request.Product.StockQuantity,
                CategoryId = request.Product.CategoryId
            };

            // 3. Call the repository to create the product
            _unitOfWork.ProductRepository.Create(product);
            await _unitOfWork.SaveChangesAsync();

            // 4. Return the ID of the created product
            return product.Id;
        }
    }
}
