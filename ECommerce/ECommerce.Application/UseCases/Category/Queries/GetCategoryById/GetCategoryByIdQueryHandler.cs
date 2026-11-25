using ECommerce.Application.Common.Exceptions;
using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.Interfaces.ReadInterfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetCategoryById
{
    public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryDto>
    {
        private readonly ICategoryReadRepository _categoryReadRepository;

        public GetCategoryByIdQueryHandler(ICategoryReadRepository categoryReadRepository)
        {
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<GetCategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryReadRepository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Category), request.Id);
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
