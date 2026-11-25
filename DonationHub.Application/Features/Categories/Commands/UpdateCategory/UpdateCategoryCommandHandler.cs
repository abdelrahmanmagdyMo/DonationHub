using DonationHub.Application.Features.Categories.Dtos;
using DonationHub.Application.Features.Categories.Mappers;
using DonationHub.Domain.Categories;
using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.Interfaces;
using DonationHub.Domain.ValueObjects;
using MediatR;
using CategoryErrors = DonationHub.Domain.Categories.Errors.CategoryErrors;

namespace DonationHub.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
    {
        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken ct)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id, ct);
            if (category is null) return Result<CategoryDto>.Failure(CategoryErrors.NotFound);
            var categoryUpdatedResult = category.Update(LocalizedText.Create(request.NameAr, request.NameEn), LocalizedText
                .Create(request.DescriptionAr, request.DescriptionEn));
            if (categoryUpdatedResult.IsFailure) return Result<CategoryDto>.Failure(categoryUpdatedResult.Error);
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync(ct);
            var categoryResponse = category.ToDto();
            return Result<CategoryDto>.Success(categoryResponse);

        }
    }
}