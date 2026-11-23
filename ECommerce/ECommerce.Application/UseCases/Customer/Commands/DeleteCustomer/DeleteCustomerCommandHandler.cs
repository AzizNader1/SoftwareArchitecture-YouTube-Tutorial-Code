using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Customer.Commands.DeleteCustomer
{
    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            // 1. Call the repository to delete the customer
            _unitOfWork.CustomerRepository.Delete(request.Id);
            await _unitOfWork.SaveChangesAsync();

            // 2. Return success status
            return true;
        }
    }
}
