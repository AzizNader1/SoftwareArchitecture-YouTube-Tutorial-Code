using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.ReadInterfaces
{
    public interface ICustomerReadRepository : IGenericReadRepository<Customer>
    {
        Task<GetCustomerDto> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUniqueAsync(string email, int? excludeCustomerId = null, CancellationToken cancellationToken = default);
    }
}
