using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<GetCustomerDto>> GetAll();
        Task<GetCustomerDto> GetById(int id);
        void Create(Customer customer);
        void Update(int id, Customer customer);
        void Delete(int id);
    }
}
