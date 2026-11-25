using ECommerce.Application.DTOs.ProductDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.ReadInterfaces
{
    public interface IProductReadRepository : IGenericReadRepository<Product>
    {
        Task<GetProductDto> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<bool> HasProductsByCategoryIdAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}
