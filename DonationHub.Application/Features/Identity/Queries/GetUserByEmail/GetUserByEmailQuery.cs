using DonationHub.Application.Authentications.Responses;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Identity.Queries.GetUserByEmail
{
    public sealed record GetUserByEmailQuery(string Email) : IRequest<Result<GetUserByEmailResponse>>;

}
