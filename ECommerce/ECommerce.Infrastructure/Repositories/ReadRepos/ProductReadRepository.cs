using ECommerce.Application.DTOs.ProductDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.ReadRepos
{
    public class ProductReadRepository : GenericReadRepository<Product>, IProductReadRepository
    {
        public ProductReadRepository(ApplicationReadDbContext context) : base(context)
        {
        }

        public async Task<GetProductDto> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var product = await DbSet
                .FirstOrDefaultAsync(p => p.Name == name, cancellationToken);

            if (product == null) return null!;

            return new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId
            };
        }

        public async Task<bool> HasProductsByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(p => p.CategoryId == categoryId, cancellationToken);
        }
    }
}
