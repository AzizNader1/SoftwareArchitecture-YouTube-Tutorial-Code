using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.CancelOrder
{
    public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IProductReadRepository _productReadRepository;

        public CancelOrderCommandHandler(
            IUnitOfWork unitOfWork,
            IOrderReadRepository orderReadRepository,
            IProductReadRepository productReadRepository)
        {
            _unitOfWork = unitOfWork;
            _orderReadRepository = orderReadRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Retrieve the order
            var order = await _orderReadRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order), request.Id);
            }

            // 2. Business rules on cancellation eligibility
            if (order.Status == OrderStatus.Cancelled)
            {
                throw new DomainException("Order is already cancelled.");
            }

            if (order.Status == OrderStatus.Delivered)
            {
                throw new DomainException("Cannot cancel an order that has already been delivered.");
            }

            if (order.Status == OrderStatus.Shipped)
            {
                throw new DomainException("Cannot cancel an order that has already been shipped.");
            }

            // 3. Begin Transaction to cancel order and restock items atomically
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                // Restock items
                if (order.OrderItems != null)
                {
                    foreach (var item in order.OrderItems)
                    {
                        var product = await _productReadRepository.GetByIdAsync(item.ProductId, cancellationToken);
                        if (product is not null)
                        {
                            product.StockQuantity += item.Quantity;
                            await _unitOfWork.ProductWriteRepository.UpdateAsync(product, cancellationToken);
                        }
                    }
                }

                // Update order status
                order.Status = OrderStatus.Cancelled;
                await _unitOfWork.OrderWriteRepository.UpdateAsync(order, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Commit transaction
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
