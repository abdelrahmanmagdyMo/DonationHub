namespace DonationHub.Domain.Exceptions
{
    public sealed class BannerTooLargeException() : DomainException("The banner file is too large!")
    {
    }
}
