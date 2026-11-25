using ECommerce.Application.Common.Models;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Common;
using ECommerce.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.ReadRepos
{
    public class GenericReadRepository<T> : IGenericReadRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationReadDbContext Context;
        protected readonly DbSet<T> DbSet;

        public GenericReadRepository(ApplicationReadDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.CountAsync(cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.AnyAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task<PaginatedList<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var totalCount = await DbSet.CountAsync(cancellationToken);
            var items = await DbSet
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<T>(items, totalCount, pageNumber, pageSize);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsyncWithoutPagination(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            return entity!;
        }
    }
}
