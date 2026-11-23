using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // 1. Call the repository to delete the product
            _unitOfWork.ProductRepository.Delete(request.Id);
            await _unitOfWork.SaveChangesAsync();

            // 2. Return success status
            return true;
        }
    }
}
