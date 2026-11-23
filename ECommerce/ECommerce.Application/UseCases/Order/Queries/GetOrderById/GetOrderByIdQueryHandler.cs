using ECommerce.Application.DTOs.OrderDTOs;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Order.Queries.GetOrderById
{
    public sealed class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.GetById(request.Id);
            if (order == null)
            {
                return null!;
            }

            return order;
        }
    }
}
