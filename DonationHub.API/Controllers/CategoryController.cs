using DonationHub.Application.Contracts;
using DonationHub.Application.Features.Category;
using DonationHub.Domain.Categories;
using DonationHub.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DonationHub.API.Controllers
{

    public class CategoryController(IUnitOfWork unitOfWork) : BaseApiController
    {

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories(CancellationToken ct)
        {
            var categories = await unitOfWork.CategoryRepository.GetAllAsync(ct);
            var categoryDto = categories.Select(c => c.ToDto()).ToList();
            return Ok(categoryDto);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id, ct);
            if (category is null) return NotFound();
            var categoryDto = category.ToDto();
            return Ok(categoryDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreate, CancellationToken ct)
        {
            var category = Category.Create(LocalizedText.Create(categoryCreate.NameArabic, categoryCreate.NameEnglish)
                , LocalizedText.Create(categoryCreate.DescriptionArabic,
                categoryCreate.DescriptionEnglish));
            await unitOfWork.CategoryRepository.AddAsync(category, ct);
            await unitOfWork.SaveChangesAsync(ct);
            return Ok();
        }
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryUpdateDto categoryCreate, CancellationToken ct)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id, ct);
            if (category is null) return NotFound();
            category.Update(LocalizedText.Create(categoryCreate.NameArabic, categoryCreate.NameEnglish)
              , LocalizedText.Create(categoryCreate.DescriptionArabic,
                categoryCreate.DescriptionEnglish));
            unitOfWork.CategoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync(ct);
            return Ok();
        }
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken ct)
        {
            var category = await unitOfWork.CategoryRepository.GetByIdAsync(id, ct);
            if (category is null) return NotFound();

            unitOfWork.CategoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync(ct);
            return Ok();
        }
    }
}
