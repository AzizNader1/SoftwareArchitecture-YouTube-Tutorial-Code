using ECommerce.Domain.Enums;

namespace ECommerce.Application.DTOs.OrderDTOs
{
    public sealed class UpdateOrderStatusDto
    {
        public OrderStatus Status { get; set; }
    }
}
