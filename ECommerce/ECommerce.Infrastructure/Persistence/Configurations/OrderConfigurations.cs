using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.TotalAmount).HasPrecision(18, 2);
            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(30);
            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // ==========================================
            // Data Seeding
            // ==========================================
            builder.HasData(
                new Order
                {
                    Id = 1,
                    OrderDate = new DateTime(2026, 1, 2, 10, 30, 0, DateTimeKind.Utc),
                    TotalAmount = 1549.98m,
                    Status = OrderStatus.Pending,
                    CustomerId = 1,
                    CreatedDate = new DateTime(2026, 1, 2, 10, 30, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
