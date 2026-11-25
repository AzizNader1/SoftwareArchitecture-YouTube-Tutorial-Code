using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetCategoryByName
{
    public sealed class GetCategoryByNameQueryHandler : IRequestHandler<GetCategoryByNameQuery, GetCategoryDto>
    {
        private readonly ICategoryReadRepository _categoryRepository;
        public GetCategoryByNameQueryHandler(ICategoryReadRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<GetCategoryDto> Handle(GetCategoryByNameQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByNameAsync(request.Name);
            if (category == null)
            {
                return null;
            }
            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
