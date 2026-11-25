using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductReadRepository _productReadRepository;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IProductReadRepository productReadRepository)
        {
            _unitOfWork = unitOfWork;
            _productReadRepository = productReadRepository;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Call the repository to delete the product
            var product = await _productReadRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);
            }
            await _unitOfWork.ProductWriteRepository.DeleteAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 2. Return success status
            return true;
        }
    }
}
