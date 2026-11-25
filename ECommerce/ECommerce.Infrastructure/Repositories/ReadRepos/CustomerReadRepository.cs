using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.ReadRepos
{
    public class CustomerReadRepository : GenericReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ApplicationReadDbContext context) : base(context)
        {
        }

        public async Task<GetCustomerDto> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var customer = await DbSet
                .FirstOrDefaultAsync(c => (c.FirstName + " " + c.LastName).Contains(name), cancellationToken);

            if (customer == null) return null!;

            return new GetCustomerDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task<bool> IsEmailUniqueAsync(string email, int? excludeCustomerId = null, CancellationToken cancellationToken = default)
        {
            var normalizedEmail = email.Trim().ToLower();
            if (excludeCustomerId.HasValue)
            {
                return !await DbSet.AnyAsync(c => c.Email.ToLower() == normalizedEmail && c.Id != excludeCustomerId.Value, cancellationToken);
            }

            return !await DbSet.AnyAsync(c => c.Email.ToLower() == normalizedEmail, cancellationToken);
        }
    }
}
