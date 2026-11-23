using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.CreateOrder
{
    public sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request through the CreateOrderCommandValidator

            // 2. Map the request DTO to the domain model
            var order = new Domain.Entities.Order
            {
                CustomerId = request.Order.CustomerId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = request.Order.Items.Select(i => new Domain.Entities.OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            // 3. Calculate total amount
            order.TotalAmount = order.OrderItems.Sum(i => i.TotalPrice);

            // 4. Call the repository to create the order
            _unitOfWork.OrderRepository.Create(order);
            await _unitOfWork.SaveChangesAsync();

            // 5. Return the ID of the created order
            return order.Id;
        }
    }
}
