using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Common;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerReadRepository _customerReadRepository;
        private readonly IProductReadRepository _productReadRepository;

        public CreateOrderCommandHandler(
            IUnitOfWork unitOfWork,
            ICustomerReadRepository customerReadRepository,
            IProductReadRepository productReadRepository)
        {
            _unitOfWork = unitOfWork;
            _customerReadRepository = customerReadRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Verify Customer exists
            var customerExists = await _customerReadRepository.ExistsAsync(request.Order.CustomerId, cancellationToken);
            if (!customerExists)
            {
                throw new NotFoundException(nameof(Customer), request.Order.CustomerId);
            }

            // 2. Begin Transaction for atomic Order creation + Stock deduction
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var orderItems = new List<OrderItem>();

                // 3. Validate products, enforce database price security, and deduct stock
                foreach (var itemDto in request.Order.Items)
                {
                    var product = await _productReadRepository.GetByIdAsync(itemDto.ProductId, cancellationToken);
                    if (product == null)
                    {
                        throw new NotFoundException(nameof(Product), itemDto.ProductId);
                    }

                    if (product.StockQuantity < itemDto.Quantity)
                    {
                        throw new DomainException($"Insufficient stock for product '{product.Name}'. Available: {product.StockQuantity}, Requested: {itemDto.Quantity}.");
                    }

                    // Deduct stock
                    product.StockQuantity -= itemDto.Quantity;
                    await _unitOfWork.ProductWriteRepository.UpdateAsync(product, cancellationToken);

                    // Add order item using the authoritative server-side price
                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = itemDto.Quantity,
                        UnitPrice = product.Price
                    });
                }

                // 4. Create and calculate Order
                var order = new Domain.Entities.Order
                {
                    CustomerId = request.Order.CustomerId,
                    OrderDate = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    OrderItems = orderItems,
                    TotalAmount = orderItems.Sum(oi => oi.TotalPrice)
                };

                await _unitOfWork.OrderWriteRepository.AddAsync(order, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // 5. Commit transaction
                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return order.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}
