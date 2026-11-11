namespace DonationHub.Domain.Exceptions
{
    public sealed class CampaignNotActiveException() : DomainException("The campaign is not active!")
    {
    }
}
