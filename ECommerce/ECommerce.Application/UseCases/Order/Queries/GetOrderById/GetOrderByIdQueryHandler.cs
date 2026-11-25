using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public GetOrderByIdQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderReadRepository.GetByIdAsync(request.Id, cancellationToken);
            if (order == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Order), request.Id);
            }

            return new GetOrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = order.Customer != null ? order.Customer.FullName : string.Empty,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Items = order.OrderItems != null 
                    ? order.OrderItems.Select(oi => new GetOrderItemDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product != null ? oi.Product.Name : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        TotalPrice = oi.TotalPrice
                    }).ToList()
                    : new List<GetOrderItemDto>()
            };
        }
    }
}
