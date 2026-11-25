using ECommerce.Application.Common.Models;
using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetAllCategories
{
    public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, PaginatedList<GetCategoryDto>>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;

        public GetAllCategoriesQueryHandler(ICategoryReadRepository categoryReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<PaginatedList<GetCategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var pagedCategories = await _categoryReadRepository.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
            if (pagedCategories == null || !pagedCategories.Items.Any())
            {
                return new PaginatedList<GetCategoryDto>(new List<GetCategoryDto>(), 0, request.PageNumber, request.PageSize);
            }

            var dtos = pagedCategories.Items.Select(c => new GetCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return new PaginatedList<GetCategoryDto>(dtos, pagedCategories.TotalCount, pagedCategories.PageNumber, pagedCategories.PageSize);
        }
    }
}
