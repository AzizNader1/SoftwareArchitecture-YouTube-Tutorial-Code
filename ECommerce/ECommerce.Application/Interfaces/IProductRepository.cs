using ECommerce.Application.DTOs.ProductDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<GetProductDto>> GetAll();
        Task<GetProductDto> GetById(int id);
        void Create(Product product);
        void Update(int id, Product product);
        void Delete(int id);
    }
}
