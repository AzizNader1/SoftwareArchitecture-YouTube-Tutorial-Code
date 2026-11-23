using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<GetCategoryDto>> GetAll();
        Task<GetCategoryDto> GetById(int id);
        void Create(Category category);
        void Update(int id, Category category);
        void Delete(int id);
    }
}
