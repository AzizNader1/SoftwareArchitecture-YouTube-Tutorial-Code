using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.ProductDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Product.Queries.GetAllProducts
{
    public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedList<GetProductDto>>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<PaginatedList<GetProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var pagedProducts = await _productReadRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (pagedProducts == null || !pagedProducts.Items.Any())
            {
                return new PaginatedList<GetProductDto>(new List<GetProductDto>(), 0, request.PageNumber, request.PageSize);
            }

            var dtos = pagedProducts.Items.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description ?? string.Empty,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId
            }).ToList();

            return new PaginatedList<GetProductDto>(dtos, pagedProducts.TotalCount, pagedProducts.PageNumber, pagedProducts.PageSize);
        }
    }
}
