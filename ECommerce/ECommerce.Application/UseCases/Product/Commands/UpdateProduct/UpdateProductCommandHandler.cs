using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;

        public UpdateProductCommandHandler(
            IUnitOfWork unitOfWork,
            IProductReadRepository productReadRepository,
            ICategoryReadRepository categoryReadRepository)
        {
            _unitOfWork = unitOfWork;
            _productReadRepository = productReadRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate that the product exists
            var productExists = await _productReadRepository.ExistsAsync(request.Id, cancellationToken);
            if (!productExists)
            {
                throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
            }

            // 2. Validate that the Category exists
            var categoryExists = await _categoryReadRepository.ExistsAsync(request.Product.CategoryId, cancellationToken);
            if (!categoryExists)
            {
                throw new NotFoundException(nameof(Domain.Entities.Category), request.Product.CategoryId);
            }

            // 3. Map the request DTO to the domain model with the proper Id
            var product = new Domain.Entities.Product
            {
                Id = request.Id,
                Name = request.Product.Name,
                Description = request.Product.Description,
                Price = request.Product.Price,
                StockQuantity = request.Product.StockQuantity,
                CategoryId = request.Product.CategoryId
            };

            // 4. Call the repository to update the product
            await _unitOfWork.ProductWriteRepository.UpdateAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Return success status
            return true;
        }
    }
}
