using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Queries.GetUserById
{
    public sealed record GetUserByIdQuery(Guid UserId) : IRequest<Result<GetUserByIdResponse>>;

}
