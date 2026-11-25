using DonationHub.Application.Features.Categories.Dtos;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<Result<IEnumerable<CategoryDto>>>
    {
    }
}
