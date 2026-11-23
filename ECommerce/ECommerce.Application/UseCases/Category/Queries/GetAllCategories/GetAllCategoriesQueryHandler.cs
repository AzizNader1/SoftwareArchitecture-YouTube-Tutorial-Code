using ECommerce.Application.DTOs.CategoryDTOs;
using ECommerce.Application.Interfaces;
using MediatR;

namespace ECommerce.Application.UseCases.Category.Queries.GetAllCategories
{
    public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<GetCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GetCategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();
            if (categories == null || !categories.Any())
            {
                return Enumerable.Empty<GetCategoryDto>();
            }

            return categories;
        }
    }
}
