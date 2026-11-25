using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories.ReadRepos
{
    public class OrderReadRepository : GenericReadRepository<Order>, IOrderReadRepository
    {
        public OrderReadRepository(ApplicationReadDbContext context) : base(context)
        {
        }

        public override async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var order = await DbSet
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);

            return order!;
        }

        public async Task<IReadOnlyList<GetOrderDto>> GetOrdersByCustomerIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(o => o.CustomerId == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.FullName : string.Empty,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Items = o.OrderItems.Select(oi => new GetOrderItemDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product != null ? oi.Product.Name : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        TotalPrice = oi.TotalPrice
                    }).ToList()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<GetOrderDto>> GetOrdersByCustomerNameAsync(string name, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(o => (o.Customer.FirstName + " " + o.Customer.LastName).Contains(name))
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .Select(o => new GetOrderDto
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer != null ? o.Customer.FullName : string.Empty,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    Items = o.OrderItems.Select(oi => new GetOrderItemDto
                    {
                        Id = oi.Id,
                        ProductId = oi.ProductId,
                        ProductName = oi.Product != null ? oi.Product.Name : string.Empty,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice,
                        TotalPrice = oi.TotalPrice
                    }).ToList()
                })
                .ToListAsync(cancellationToken);
        }
    }
}
