using DonationHub.Application.Features.Categories.Dtos;
using DonationHub.Application.Features.Categories.Mappers;
using DonationHub.Domain.Categories;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Interfaces;
using DonationHub.Domain.ValueObjects;
using MediatR;
using domainCategory = DonationHub.Domain.Categories.Category;
namespace DonationHub.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryCommand, Result<CategoryDto>>
    {
        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken ct)
        {
            var categoryResult = domainCategory.Create(LocalizedText.Create(request.NameAr, request.NameEn), LocalizedText
                .Create(request.DescriptionAr, request.DescriptionEn));
            if (categoryResult.IsFailure) return Result<CategoryDto>.Failure(categoryResult.Error);
            var categoryResponse = categoryResult.Value.ToDto();
            await categoryRepository.AddAsync(categoryResult.Value, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return Result<CategoryDto>.Success(categoryResponse);
        }
    }
}
