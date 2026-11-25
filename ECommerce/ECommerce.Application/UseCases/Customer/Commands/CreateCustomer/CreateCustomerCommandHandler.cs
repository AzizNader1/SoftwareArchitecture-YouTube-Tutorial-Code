using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Common;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerReadRepository _customerReadRepository;

        public CreateCustomerCommandHandler(
            IUnitOfWork unitOfWork,
            ICustomerReadRepository customerReadRepository)
        {
            _unitOfWork = unitOfWork;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Business rule: Ensure email is unique
            var isEmailUnique = await _customerReadRepository.IsEmailUniqueAsync(request.Customer.Email, null, cancellationToken);
            if (!isEmailUnique)
            {
                throw new DomainException($"A customer with the email '{request.Customer.Email}' already exists.");
            }

            // 2. Map the request DTO to the domain model
            var customer = new Domain.Entities.Customer
            {
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                Email = request.Customer.Email.Trim().ToLowerInvariant(),
                PhoneNumber = request.Customer.PhoneNumber
            };

            // 3. Call the repository to create the customer
            await _unitOfWork.CustomerWriteRepository.AddAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 4. Return the ID of the created customer
            return customer.Id;
        }
    }
}
