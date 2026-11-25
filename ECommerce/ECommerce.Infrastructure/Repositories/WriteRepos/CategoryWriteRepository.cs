using ECommerce.Application.Interfaces.WriteInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;

namespace ECommerce.Infrastructure.Repositories.WriteRepos
{
    public class CategoryWriteRepository : GenericWriteRepository<Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(ApplicationWriteDbContext context) : base(context)
        {
        }
    }
}
