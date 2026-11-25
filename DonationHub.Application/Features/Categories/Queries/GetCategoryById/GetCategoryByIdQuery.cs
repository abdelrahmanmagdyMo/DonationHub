using DonationHub.Application.Features.Categories.Dtos;
using DonationHub.Domain.Common.Results.Abstractions;
using MediatR;

namespace DonationHub.Application.Features.Categories.Commands.CreateCategory
{
    public class GetCategoryByIdQuery : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; set; }
        public GetCategoryByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
