using ECommerce.Application.Interfaces.WriteInterfaces;

namespace ECommerce.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductWriteRepository ProductWriteRepository { get; }
        ICategoryWriteRepository CategoryWriteRepository { get; }
        ICustomerWriteRepository CustomerWriteRepository { get; }
        IOrderWriteRepository OrderWriteRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    }
}
