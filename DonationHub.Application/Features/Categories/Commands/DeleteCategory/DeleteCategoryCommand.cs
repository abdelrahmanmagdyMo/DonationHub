using DonationHub.Domain.Common.Results;
using MediatR;

namespace DonationHub.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed record DeleteCategoryCommand(Guid Id) : IRequest<Result>;

}
