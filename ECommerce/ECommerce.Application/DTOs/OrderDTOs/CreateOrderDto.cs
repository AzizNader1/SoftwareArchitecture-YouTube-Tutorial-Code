namespace ECommerce.Application.DTOs.OrderDTOs
{
    public sealed class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
