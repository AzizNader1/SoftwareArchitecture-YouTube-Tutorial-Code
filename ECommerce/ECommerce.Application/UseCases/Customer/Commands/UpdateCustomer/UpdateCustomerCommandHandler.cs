using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.UpdateCustomer
{
    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate the request through the UpdateCustomerCommandValidator

            // 2. Map the request DTO to the domain model
            var customer = new Domain.Entities.Customer
            {
                Id = request.Id,
                FirstName = request.Customer.FirstName,
                LastName = request.Customer.LastName,
                Email = request.Customer.Email,
                PhoneNumber = request.Customer.PhoneNumber
            };

            // 3. Call the repository to update the customer
            _unitOfWork.CustomerRepository.Update(request.Id, customer);
            await _unitOfWork.SaveChangesAsync();

            // 4. Return success status
            return true;
        }
    }
}
