namespace DonationHub.Domain.Exceptions
{
    public sealed class Money_Amount_Exception() : DomainException("Amount Can Not Be Less Than Zero.")
    {
    }
}
