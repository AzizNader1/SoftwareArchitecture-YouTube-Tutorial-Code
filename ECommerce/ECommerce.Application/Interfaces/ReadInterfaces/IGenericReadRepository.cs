using ECommerce.Application.Common.Models;
using ECommerce.Domain.Common;

namespace ECommerce.Application.Interfaces.ReadInterfaces
{
    public interface IGenericReadRepository<T> where T : BaseEntity
    {
        Task<PaginatedList<T>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<T>> GetAllAsyncWithoutPagination(CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}
