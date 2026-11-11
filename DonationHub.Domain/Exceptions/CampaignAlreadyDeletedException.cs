namespace DonationHub.Domain.Exceptions
{
    public sealed class CampaignAlreadyDeletedException() : DomainException("The campaign was already deleted!")
    {
    }
}
