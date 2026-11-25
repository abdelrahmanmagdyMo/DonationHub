using DonationHub.Application.Features.Categories.Dtos;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Categories.Commands.CreateCategory
{
    public sealed record CreateCategoryCommand(string NameAr, string NameEn, string DescriptionAr, string DescriptionEn) : IRequest<Result<CategoryDto>>;
}