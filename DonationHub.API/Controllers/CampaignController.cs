using DonationHub.Application.Features.Campaigns.Commands.CreateCampaign;
using DonationHub.Application.Features.Campaigns.Commands.DeleteCampaign;
using DonationHub.Application.Features.Campaigns.Commands.UpdateCampaign;
using DonationHub.Application.Features.Campaigns.Queries.GetCampaignById;
using DonationHub.Application.Features.Campaigns.Queries.GetCampaigns;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonationHub.API.Controllers
{

    public class CampaignController(ISender sender) : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("Campaigns")]
        public async Task<IActionResult> GetCampaigns([FromBody] GetCampaignsQuery query, CancellationToken ct) =>
            ToActionResult(await sender.Send(query, ct));

        [AllowAnonymous]
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetCampaignById(Guid id, CancellationToken ct)
            => ToActionResult(await sender.Send(new GetCampaignByIdQuery(id), ct));


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignCommand cmd, CancellationToken ct)
            => ToActionResult(await sender.Send(cmd, ct));


        [Authorize(Roles = "Admin")]
        [HttpPut("id:guid")]
        public async Task<IActionResult> UpdateCampaign(Guid id, [FromBody] UpdateCampaignCommand cmd, CancellationToken ct)
            => ToActionResult(await sender.Send(cmd, ct));

        [Authorize(Roles = "Admin")]
        [HttpDelete("id:guid")]
        public async Task<IActionResult> DeleteCampaign(Guid id, CancellationToken ct)
            => ToActionResult(await sender.Send(new DeleteCampaignCommand(id), ct));
    }
}
