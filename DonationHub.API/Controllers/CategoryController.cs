
using DonationHub.Application.Features.Categories.Commands.CreateCategory;
using DonationHub.Application.Features.Categories.Commands.DeleteCategory;
using DonationHub.Application.Features.Categories.Commands.UpdateCategory;
using DonationHub.Application.Features.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonationHub.API.Controllers
{

    public class CategoryController(IMediator mediator) : BaseApiController
    {

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories(CancellationToken ct)
        {
            var query = new GetCategoriesQuery();
            var categories = await mediator.Send(query, ct);
            return categories.IsFailure ? NoContent() : Ok(categories.Value);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var query = new GetCategoryByIdQuery(id);
            var category = await mediator.Send(query, ct);
            return category.IsFailure ? NotFound() : Ok(category.Value);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand cmd, CancellationToken ct)
        {
            var categoryResult = await mediator.Send(cmd, ct);
            return categoryResult.IsFailure ? BadRequest() : Ok(categoryResult.Value);
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand cmd, CancellationToken ct)
        {
            var categoryResult = await mediator.Send(cmd with { Id = id }, ct);
            return categoryResult.IsFailure ? NotFound() : Ok(categoryResult.Value);
        }
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken ct)
        {
            var cmd = new DeleteCategoryCommand(id);
            var deleted = await mediator.Send(cmd, ct);
            return deleted.IsFailure ? NotFound() : Ok();
        }
    }
}
