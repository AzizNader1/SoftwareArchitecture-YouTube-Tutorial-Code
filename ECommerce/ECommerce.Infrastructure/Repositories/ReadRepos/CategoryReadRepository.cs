using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.ReadRepos
{
    public class CategoryReadRepository : GenericReadRepository<Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(ApplicationReadDbContext context) : base(context)
        {
        }

        public async Task<GetCategoryDto> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var category = await DbSet
                .FirstOrDefaultAsync(c => c.Name == name, cancellationToken);

            if (category == null) return null!;

            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
