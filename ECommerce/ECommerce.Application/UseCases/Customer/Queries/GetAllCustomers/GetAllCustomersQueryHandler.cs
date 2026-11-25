using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.CustomerDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Queries.GetAllCustomers
{
    public sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, PaginatedList<GetCustomerDto>>
    {
        private readonly ICustomerReadRepository _customerReadRepository;

        public GetAllCustomersQueryHandler(ICustomerReadRepository customerReadRepository)
        {
            _customerReadRepository = customerReadRepository;
        }

        public async Task<PaginatedList<GetCustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var pagedCustomers = await _customerReadRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (pagedCustomers == null || !pagedCustomers.Items.Any())
            {
                return new PaginatedList<GetCustomerDto>(new List<GetCustomerDto>(), 0, request.PageNumber, request.PageSize);
            }

            var dtos = pagedCustomers.Items.Select(c => new GetCustomerDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber
            }).ToList();

            return new PaginatedList<GetCustomerDto>(dtos, pagedCustomers.TotalCount, pagedCustomers.PageNumber, pagedCustomers.PageSize);
        }
    }
}
