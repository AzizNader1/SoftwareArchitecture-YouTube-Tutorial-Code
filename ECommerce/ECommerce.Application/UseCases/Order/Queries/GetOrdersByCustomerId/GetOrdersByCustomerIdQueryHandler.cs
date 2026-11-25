using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrdersByCustomerId
{
    public sealed class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<GetOrderDto>>
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public GetOrdersByCustomerIdQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<IEnumerable<GetOrderDto>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderReadRepository.GetOrdersByCustomerIdAsync(request.CustomerId, cancellationToken);
            if (orders == null || !orders.Any())
            {
                return Enumerable.Empty<GetOrderDto>();
            }

            return orders;
        }
    }
}
