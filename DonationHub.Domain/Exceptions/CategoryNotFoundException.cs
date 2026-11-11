namespace DonationHub.Domain.Exceptions
{
    public sealed class CategoryNotFoundException() : DomainException("This Category Is Not Found!")
    {
    }
}
