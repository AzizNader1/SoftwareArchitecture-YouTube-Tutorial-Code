using ECommerce.Application.Interfaces;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.CancelOrder
{
    public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            // 1. Retrieve the order from the repository
            var orderDto = await _unitOfWork.OrderRepository.GetById(request.Id);
            if (orderDto is null)
            {
                return false;
            }

            // 2. Set status to Cancelled
            var order = new Domain.Entities.Order
            {
                Id = request.Id,
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                Status = OrderStatus.Cancelled
            };

            // 3. Update the order
            _unitOfWork.OrderRepository.Update(request.Id, order);
            await _unitOfWork.SaveChangesAsync();

            // 4. Return success status
            return true;
        }
    }
}
