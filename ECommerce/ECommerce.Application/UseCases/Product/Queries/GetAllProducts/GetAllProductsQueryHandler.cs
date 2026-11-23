using ECommerce.Application.DTOs.ProductDTOs;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Queries.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<GetProductDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.ProductRepository.GetAll();
            if (products == null || !products.Any())
            {
                return Enumerable.Empty<GetProductDto>();
            }

            return products;
        }
    }
}
