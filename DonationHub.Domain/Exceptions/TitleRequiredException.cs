namespace DonationHub.Domain.Exceptions
{
    public sealed class TitleRequiredException() : DomainException("The title is required in Arabic and English.")
    {
    }
}
