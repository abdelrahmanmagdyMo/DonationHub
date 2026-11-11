namespace DonationHub.Domain.Exceptions
{
    public sealed class NotOwnerException() : DomainException("You can only manage campaigns you created.")
    {
    }
}
