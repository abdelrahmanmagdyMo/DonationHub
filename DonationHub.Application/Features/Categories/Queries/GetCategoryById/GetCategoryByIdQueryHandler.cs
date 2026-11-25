using DonationHub.Application.Features.Categories.Dtos;
using DonationHub.Application.Features.Categories.Mappers;
using DonationHub.Domain.Categories;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;
using CategoryErrors = DonationHub.Domain.Categories.Errors;
namespace DonationHub.Application.Features.Categories.Commands.CreateCategory
{
    public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
    {
        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken ct)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id, ct);
            if (category is null) return Result<CategoryDto>.Failure(CategoryErrors.CategoryErrors.NotFound);
            var categoryResponse = category.ToDto();
            return Result<CategoryDto>.Success(categoryResponse);
        }
    }
}
