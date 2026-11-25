using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Domain.Common;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.UpdateCustomer
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerReadRepository _customerReadRepository;

        public UpdateCustomerCommandHandler(
            IUnitOfWork unitOfWork,
            ICustomerReadRepository customerReadRepository)
        {
            _unitOfWork = unitOfWork;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate that the customer exists
            var customerExists = await _customerReadRepository.ExistsAsync(request.Id, cancellationToken);
            if (!customerExists)
            {
                throw new NotFoundException(nameof(Domain.Entities.Customer), request.Id);
            }

            // 2. Business rule: Ensure email is unique across other customers
            var isEmailUnique = await _customerReadRepository.IsEmailUniqueAsync(request.Customer.Email, request.Id, cancellationToken);
            if (!isEmailUnique)
            {
                throw new DomainException($"A customer with the email '{request.Customer.Email}' already exists.");
            }

            // 3. Map the request DTO to the domain model
            var customer = new Domain.Entities.Customer
            {
                Id = request.Id,
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                Email = request.Customer.Email.Trim().ToLowerInvariant(),
                PhoneNumber = request.Customer.PhoneNumber
            };

            // 4. Call the repository to update the customer
            await _unitOfWork.CustomerWriteRepository.UpdateAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 5. Return success status
            return true;
        }
    }
}
