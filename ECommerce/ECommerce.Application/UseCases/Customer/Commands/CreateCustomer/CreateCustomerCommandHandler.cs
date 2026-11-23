using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.CreateCustomer
{
    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request through the CreateCustomerCommandValidator

            // 2. Map the request DTO to the domain model
            var customer = new Domain.Entities.Customer
            {
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                Email = request.Customer.Email,
                PhoneNumber = request.Customer.PhoneNumber
            };

            // 3. Call the repository to create the customer
            _unitOfWork.CustomerRepository.Create(customer);
            await _unitOfWork.SaveChangesAsync();

            // 4. Return the ID of the created customer
            return customer.Id;
        }
    }
}
