using System.ComponentModel.DataAnnotations;

namespace DonationHub.Application.Features.Category
{
    public record CategoryUpdateDto(
        [Required, MaxLength(100)] string NameArabic,
        [Required, MaxLength(100)] string NameEnglish,
        [Required, MaxLength(255)] string DescriptionArabic,
        [Required, MaxLength(255)] string DescriptionEnglish
        );

}
