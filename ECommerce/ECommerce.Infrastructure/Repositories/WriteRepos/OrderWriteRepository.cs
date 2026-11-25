using ECommerce.Application.Interfaces.WriteInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;

namespace ECommerce.Infrastructure.Repositories.WriteRepos
{
    public class OrderWriteRepository : GenericWriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ApplicationWriteDbContext context) : base(context)
        {
        }
    }
}
