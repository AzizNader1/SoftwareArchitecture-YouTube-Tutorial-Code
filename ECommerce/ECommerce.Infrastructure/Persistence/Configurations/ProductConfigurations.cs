using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(p => p.Price).HasPrecision(18, 2);
            builder.Property(p => p.StockQuantity).IsRequired();
            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // ==========================================
            // Data Seeding
            // ==========================================
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Name = "High-Performance Laptop",
                    Description = "16-inch display, 32GB RAM, 1TB NVMe SSD",
                    Price = 1299.99m,
                    StockQuantity = 50,
                    CategoryId = 1,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 2,
                    Name = "Wireless Noise-Canceling Headphones",
                    Description = "Over-ear headphones with active noise cancellation and 40h battery life",
                    Price = 249.99m,
                    StockQuantity = 120,
                    CategoryId = 1,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 3,
                    Name = "Cotton Crewneck T-Shirt",
                    Description = "100% premium combed cotton slim-fit t-shirt",
                    Price = 29.99m,
                    StockQuantity = 200,
                    CategoryId = 2,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Product
                {
                    Id = 4,
                    Name = "Espresso Coffee Machine",
                    Description = "15-bar Italian pump espresso maker with integrated milk frother",
                    Price = 189.50m,
                    StockQuantity = 40,
                    CategoryId = 3,
                    CreatedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}
