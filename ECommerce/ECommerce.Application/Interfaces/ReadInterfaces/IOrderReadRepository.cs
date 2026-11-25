using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.ReadInterfaces
{
    public interface IOrderReadRepository : IGenericReadRepository<Order>
    {
        Task<IReadOnlyList<GetOrderDto>> GetOrdersByCustomerIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<GetOrderDto>> GetOrdersByCustomerNameAsync(string name, CancellationToken cancellationToken = default);

    }
}
