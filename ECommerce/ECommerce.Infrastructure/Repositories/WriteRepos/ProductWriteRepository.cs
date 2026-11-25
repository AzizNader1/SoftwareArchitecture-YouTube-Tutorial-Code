using ECommerce.Application.Interfaces.WriteInterfaces;
using ECommerce.Infrastructure.Persistence.Contexts;

namespace ECommerce.Infrastructure.Repositories.WriteRepos
{
    public class ProductWriteRepository : GenericWriteRepository<Domain.Entities.Product>, IProductWriteRepository
    {
        private readonly ApplicationWriteDbContext _context;
        public ProductWriteRepository(ApplicationWriteDbContext context) : base(context)
        {
            _context = context;
        }
    }
}