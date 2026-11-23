using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<GetOrderDto>> GetAll();
        Task<GetOrderDto> GetById(int id);
        Task<IEnumerable<GetOrderDto>> GetByCustomerId(int customerId);
        void Create(Order order);
        void Update(int id, Order order);
        void Delete(int id);
    }
}
