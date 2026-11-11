namespace DonationHub.Domain.Exceptions
{
    public sealed class EndDateRequiredException() : DomainException("The end date is required")
    {
    }
}
