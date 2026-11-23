using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Queries.GetCustomerById
{
    public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetCustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetById(request.Id);
            if (customer == null)
            {
                return null!;
            }

            return customer;
        }
    }
}
