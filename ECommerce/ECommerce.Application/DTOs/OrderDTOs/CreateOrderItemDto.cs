namespace ECommerce.Application.DTOs.OrderDTOs
{
    public sealed class CreateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
