
using DonationHub.Application.Features.Categories.Commands.CreateCategory;
using DonationHub.Application.Features.Categories.Commands.DeleteCategory;
using DonationHub.Application.Features.Categories.Commands.UpdateCategory;
using DonationHub.Application.Features.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonationHub.API.Controllers
{

    public class CategoryController(ISender sender) : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories(CancellationToken ct)
             => ToActionResult(await sender.Send(new GetCategoriesQuery(), ct));

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
            => ToActionResult(await sender.Send(new GetCategoryByIdQuery(id), ct));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand cmd, CancellationToken ct)
        => ToActionResult(await sender.Send(cmd, ct));


        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand cmd, CancellationToken ct)
        => ToActionResult(await sender.Send(cmd with { Id = id }, ct));


        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken ct)
        => ToActionResult(await sender.Send(new DeleteCategoryCommand(id), ct));
    }
}
