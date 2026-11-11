namespace DonationHub.Domain.Exceptions
{
    public sealed class EndDateInPastException() : DomainException("The end date must be in the future!")
    {
    }
}
