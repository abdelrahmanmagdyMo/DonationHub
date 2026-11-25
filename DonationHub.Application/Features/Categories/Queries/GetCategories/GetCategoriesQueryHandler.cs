using DonationHub.Application.Features.Categories.Dtos;
using DonationHub.Application.Features.Categories.Mappers;
using DonationHub.Domain.Categories;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;
namespace DonationHub.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, Result<IEnumerable<CategoryDto>>>
    {
        public async Task<Result<IEnumerable<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken ct)
        {
            var categories = await categoryRepository.GetAllAsync(ct);

            return Result<IEnumerable<CategoryDto>>.Success(categories.ToDtos());
        }
    }
}
