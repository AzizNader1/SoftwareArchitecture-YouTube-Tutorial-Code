using ECommerce.Domain.Common;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities
{
    // ==========================================
    // 1) Anemic Domain Model (Active)
    // ==========================================
    public sealed class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

/*
// ==========================================
// 2) Rich Domain Model (DDD Pattern / Aggregate Root)
// ==========================================
namespace ECommerce.Domain.Entities
{
    public sealed class Order : BaseEntity
    {
        private readonly List<OrderItem> _orderItems = new();

        private Order() { }

        private Order(int customerId)
        {
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
            TotalAmount = 0;
        }

        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public OrderStatus Status { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; } = null!;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public static Order Create(int customerId)
        {
            if (customerId <= 0)
            {
                throw new DomainException("Valid customer ID is required.");
            }

            return new Order(customerId);
        }

        public void AddOrderItem(int productId, int quantity, decimal unitPrice)
        {
            EnsureOrderIsPending("add items to");

            var existingItem = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem is not null)
            {
                existingItem.AddQuantity(quantity);
            }
            else
            {
                var newItem = OrderItem.Create(productId, quantity, unitPrice);
                _orderItems.Add(newItem);
            }

            RecalculateTotalAmount();
            SetUpdatedDate();
        }

        public void RemoveOrderItem(int productId)
        {
            EnsureOrderIsPending("remove items from");

            var item = _orderItems.FirstOrDefault(i => i.ProductId == productId);
            if (item is null)
            {
                throw new DomainException($"Product with ID {productId} does not exist in this order.");
            }

            _orderItems.Remove(item);
            RecalculateTotalAmount();
            SetUpdatedDate();
        }

        public void MarkAsShipped()
        {
            if (Status != OrderStatus.Pending)
            {
                throw new DomainException($"Only pending orders can be shipped. Current status: {Status}.");
            }

            if (!_orderItems.Any())
            {
                throw new DomainException("Cannot ship an order with no items.");
            }

            Status = OrderStatus.Shipped;
            SetUpdatedDate();
        }

        public void MarkAsDelivered()
        {
            if (Status != OrderStatus.Shipped)
            {
                throw new DomainException($"Only shipped orders can be marked as delivered. Current status: {Status}.");
            }

            Status = OrderStatus.Delivered;
            SetUpdatedDate();
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Delivered)
            {
                throw new DomainException("Cannot cancel an order that has already been delivered.");
            }

            if (Status == OrderStatus.Cancelled)
            {
                throw new DomainException("Order is already cancelled.");
            }

            Status = OrderStatus.Cancelled;
            SetUpdatedDate();
        }

        private void RecalculateTotalAmount()
        {
            TotalAmount = _orderItems.Sum(item => item.TotalPrice);
        }

        private void EnsureOrderIsPending(string action)
        {
            if (Status != OrderStatus.Pending)
            {
                throw new DomainException($"Cannot {action} an order when status is '{Status}'.");
            }
        }
    }
}
*/
