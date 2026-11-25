using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Commands.UpdateOrderStatus
{
    public sealed class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderReadRepository _orderReadRepository;

        public UpdateOrderStatusCommandHandler(
            IUnitOfWork unitOfWork,
            IOrderReadRepository orderReadRepository)
        {
            _unitOfWork = unitOfWork;
            _orderReadRepository = orderReadRepository;
        }

        public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            // 1. Retrieve the existing order
            var order = await _orderReadRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order is null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order), request.Id);
            }

            // 2. Business rules for state transitions
            if (order.Status == OrderStatus.Delivered)
            {
                throw new DomainException("Cannot modify status of an order that has already been delivered.");
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                throw new DomainException("Cannot modify status of a cancelled order.");
            }

            // 3. Apply new status
            order.Status = request.StatusDto.Status;

            // 4. Update the order
            await _unitOfWork.OrderWriteRepository.UpdateAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Return success status
            return true;
        }
    }
}
