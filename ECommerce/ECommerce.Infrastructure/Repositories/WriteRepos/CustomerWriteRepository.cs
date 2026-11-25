using ECommerce.Application.Interfaces.WriteInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;

namespace ECommerce.Infrastructure.Repositories.WriteRepos
{
    public class CustomerWriteRepository : GenericWriteRepository<Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(ApplicationWriteDbContext context) : base(context)
        {
        }
    }
}
