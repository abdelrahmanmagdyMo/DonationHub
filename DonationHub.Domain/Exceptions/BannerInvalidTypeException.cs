namespace DonationHub.Domain.Exceptions
{
    public sealed class BannerInvalidTypeException() : DomainException("The banner must be a JPG, PNG or WEBP image!")
    {
    }
}
