using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    // ==========================================
    // 1) Anemic Domain Model (Active)
    // ==========================================
    public sealed class OrderItem : BaseEntity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

/*
// ==========================================
// 2) Rich Domain Model (DDD Pattern)
// ==========================================
namespace ECommerce.Domain.Entities
{
    public sealed class OrderItem : BaseEntity
    {
        private OrderItem() { }

        internal OrderItem(int productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public int OrderId { get; private set; }
        public Order Order { get; private set; } = null!;
        public int ProductId { get; private set; }
        public Product Product { get; private set; } = null!;
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        internal static OrderItem Create(int productId, int quantity, decimal unitPrice)
        {
            ValidateProductId(productId);
            ValidateQuantity(quantity);
            ValidateUnitPrice(unitPrice);

            return new OrderItem(productId, quantity, unitPrice);
        }

        internal void AddQuantity(int quantity)
        {
            ValidateQuantity(quantity);
            Quantity += quantity;
            SetUpdatedDate();
        }

        internal void UpdateQuantity(int quantity)
        {
            ValidateQuantity(quantity);
            Quantity = quantity;
            SetUpdatedDate();
        }

        private static void ValidateProductId(int productId)
        {
            if (productId <= 0)
            {
                throw new DomainException("Valid product ID is required.");
            }
        }

        private static void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("Order item quantity must be greater than zero.");
            }
        }

        private static void ValidateUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
            {
                throw new DomainException("Order item unit price must be greater than zero.");
            }
        }
    }
}
*/
