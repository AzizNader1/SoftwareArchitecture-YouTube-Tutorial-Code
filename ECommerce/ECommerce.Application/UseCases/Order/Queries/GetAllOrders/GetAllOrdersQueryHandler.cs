using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetAllOrders
{
    public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, PaginatedList<GetOrderDto>>
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public GetAllOrdersQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<PaginatedList<GetOrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var pagedOrders = await _orderReadRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (pagedOrders == null || !pagedOrders.Items.Any())
            {
                return new PaginatedList<GetOrderDto>(new List<GetOrderDto>(), 0, request.PageNumber, request.PageSize);
            }

            var dtos = pagedOrders.Items.Select(o => new GetOrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer != null ? o.Customer.FullName : string.Empty,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                Items = o.OrderItems != null
                    ? o.OrderItems.Select(oi => new GetOrderItemDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product != null ? oi.Product.Name : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        TotalPrice = oi.TotalPrice
                    }).ToList()
                    : new List<GetOrderItemDto>()
            }).ToList();

            return new PaginatedList<GetOrderDto>(dtos, pagedOrders.TotalCount, pagedOrders.PageNumber, pagedOrders.PageSize);
        }
    }
}
