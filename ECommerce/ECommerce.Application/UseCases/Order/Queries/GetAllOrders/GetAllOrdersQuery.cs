using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.OrderDTOs;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetAllOrders
{
    public sealed record GetAllOrdersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<PaginatedList<GetOrderDto>>
    {
    }
}
