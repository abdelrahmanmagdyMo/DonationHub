namespace DonationHub.Domain.Exceptions
{
    public sealed class DescriptionRequiredException() : DomainException("The description is required in Arabic and English!")
    {
    }
}
