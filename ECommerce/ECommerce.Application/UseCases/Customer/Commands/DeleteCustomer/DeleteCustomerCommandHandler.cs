using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.DeleteCustomer
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerReadRepository _customerReadRepository;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork, ICustomerReadRepository customerReadRepository)
        {
            _unitOfWork = unitOfWork;
            _customerReadRepository = customerReadRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Call the repository to delete the customer
            var customer = await _customerReadRepository.GetByIdAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Customer), request.Id);
            }
            await _unitOfWork.CustomerWriteRepository.DeleteAsync(customer, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // 2. Return success status
            return true;
        }
    }
}
