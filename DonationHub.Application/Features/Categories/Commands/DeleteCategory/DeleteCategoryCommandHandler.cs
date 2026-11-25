using DonationHub.Domain.Categories;
using DonationHub.Domain.Common.Results;
using DonationHub.Domain.Interfaces;
using MediatR;
using CategoryErrors = DonationHub.Domain.Categories.Errors.CategoryErrors;
namespace DonationHub.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryCommand, Result>

    {
        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken ct)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id, ct);
            if (category is null)
                return Result.Failure(CategoryErrors.NotFound);
            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync(ct);
            return Result.Success();
        }
    }
}
