namespace DonationHub.Domain.Exceptions
{
    public sealed class Money_CurrencyException() : DomainException("The Currency Must Be 3 characters")
    {
    }
}
