using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.UnitPrice).HasPrecision(18, 2);
            builder.Ignore(oi => oi.TotalPrice);
            builder.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // Data Seeding
            // ==========================================
            builder.HasData(
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    ProductId = 1, // High-Performance Laptop
                    Quantity = 1,
                    UnitPrice = 1299.99m,
                    CreatedDate = new DateTime(2026, 1, 2, 10, 30, 0, DateTimeKind.Utc)
                },
                new OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 2, // Wireless Noise-Canceling Headphones
                    Quantity = 1,
                    UnitPrice = 249.99m,
                    CreatedDate = new DateTime(2026, 1, 2, 10, 30, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
