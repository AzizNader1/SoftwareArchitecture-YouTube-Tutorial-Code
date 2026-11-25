using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.ReadInterfaces
{
    public interface ICategoryReadRepository : IGenericReadRepository<Category>
    {
        Task<GetCategoryDto> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
