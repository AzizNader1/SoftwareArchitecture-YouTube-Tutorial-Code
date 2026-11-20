using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities
{
    // ==========================================
    // 1) Anemic Domain Model (Active)
    // ==========================================
    public sealed class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

/*
// ==========================================
// 2) Rich Domain Model (DDD Pattern)
// ==========================================
namespace ECommerce.Domain.Entities
{
    public sealed class Product : BaseEntity
    {
        private readonly List<OrderItem> _orderItems = new();

        private Product() { }

        private Product(string name, string? description, decimal price, int stockQuantity, int categoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CategoryId = categoryId;
        }

        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; } = null!;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public static Product Create(string name, string? description, decimal price, int stockQuantity, int categoryId)
        {
            ValidateName(name);
            ValidatePrice(price);
            ValidateStockQuantity(stockQuantity);
            ValidateCategoryId(categoryId);

            return new Product(name, description, price, stockQuantity, categoryId);
        }

        public void UpdateDetails(string name, string? description)
        {
            ValidateName(name);
            Name = name;
            Description = description;
            SetUpdatedDate();
        }

        public void UpdatePrice(decimal newPrice)
        {
            ValidatePrice(newPrice);
            Price = newPrice;
            SetUpdatedDate();
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("Added stock quantity must be greater than zero.");
            }

            StockQuantity += quantity;
            SetUpdatedDate();
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new DomainException("Removed stock quantity must be greater than zero.");
            }

            if (StockQuantity < quantity)
            {
                throw new DomainException($"Insufficient stock for product '{Name}'. Available: {StockQuantity}, Requested: {quantity}.");
            }

            StockQuantity -= quantity;
            SetUpdatedDate();
        }

        public void AssignCategory(int categoryId)
        {
            ValidateCategoryId(categoryId);
            CategoryId = categoryId;
            SetUpdatedDate();
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Product name is required.");
            }

            if (name.Length > 200)
            {
                throw new DomainException("Product name cannot exceed 200 characters.");
            }
        }

        private static void ValidatePrice(decimal price)
        {
            if (price <= 0)
            {
                throw new DomainException("Product price must be greater than zero.");
            }
        }

        private static void ValidateStockQuantity(int stockQuantity)
        {
            if (stockQuantity < 0)
            {
                throw new DomainException("Stock quantity cannot be negative.");
            }
        }

        private static void ValidateCategoryId(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new DomainException("Valid category ID is required.");
            }
        }
    }
}
*/
