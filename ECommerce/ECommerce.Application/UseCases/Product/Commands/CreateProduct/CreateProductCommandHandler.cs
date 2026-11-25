using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryReadRepository _categoryReadRepository;

        public CreateProductCommandHandler(
            IUnitOfWork unitOfWork,
            ICategoryReadRepository categoryReadRepository)
        {
            _unitOfWork = unitOfWork;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request through the CreateProductCommandValidator (handled by pipeline)

            // 2. Validate business rules: ensure the Category exists
            var categoryExists = await _categoryReadRepository.ExistsAsync(request.Product.CategoryId, cancellationToken);
            if (!categoryExists)
            {
                throw new NotFoundException(nameof(Domain.Entities.Category), request.Product.CategoryId);
            }

            // 3. Map the request DTO to the domain model
            var product = new Domain.Entities.Product
            {
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                StockQuantity = request.Product.StockQuantity,
                CategoryId = request.Product.CategoryId
            };

            // 4. Call the repository to create the product
            await _unitOfWork.ProductWriteRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Return the ID of the created product
            return product.Id;
        }
    }
}
