using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrdersByCustomerId
{
    public sealed class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<GetOrderDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrdersByCustomerIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetOrderDto>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.OrderRepository.GetByCustomerId(request.CustomerId);
            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<GetOrderDto>();
            }

            return orders;
        }
    }
}
