using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.WriteInterfaces;
using ECommerce.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerce.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationWriteDbContext _context;
        private IDbContextTransaction? _transaction;

        public IProductWriteRepository ProductWriteRepository { get; }
        public ICategoryWriteRepository CategoryWriteRepository { get; }
        public ICustomerWriteRepository CustomerWriteRepository { get; }
        public IOrderWriteRepository OrderWriteRepository { get; }

        public UnitOfWork(
            ApplicationWriteDbContext context,
            IProductWriteRepository productWriteRepository,
            ICategoryWriteRepository categoryWriteRepository,
            ICustomerWriteRepository customerWriteRepository,
            IOrderWriteRepository orderWriteRepository)
        {
            _context = context;
            ProductWriteRepository = productWriteRepository;
            CategoryWriteRepository = categoryWriteRepository;
            CustomerWriteRepository = customerWriteRepository;
            OrderWriteRepository = orderWriteRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                if (_transaction != null)
                {
                    await _transaction.CommitAsync(cancellationToken);
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync(cancellationToken);
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
