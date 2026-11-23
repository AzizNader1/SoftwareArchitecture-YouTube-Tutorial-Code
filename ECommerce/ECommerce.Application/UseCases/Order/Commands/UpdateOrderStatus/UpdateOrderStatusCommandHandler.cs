using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.UpdateOrderStatus
{
    public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request through the UpdateOrderStatusCommandValidator

            // 2. Retrieve the order from the repository
            var orderDto = await _unitOfWork.OrderRepository.GetById(request.Id);
            if (orderDto is null)
            {
                return false;
            }

            // 3. Map updated status to the domain model
            var order = new Domain.Entities.Order
            {
                Id = request.Id,
                CustomerId = orderDto.CustomerId,
                OrderDate = orderDto.OrderDate,
                TotalAmount = orderDto.TotalAmount,
                Status = request.StatusDto.Status
            };

            // 4. Update the order
            _unitOfWork.OrderRepository.Update(request.Id, order);
            await _unitOfWork.SaveChangesAsync();

            // 5. Return success status
            return true;
        }
    }
}
