namespace DonationHub.Domain.Exceptions
{
    public sealed class CampaignAlreadyFinalException() : DomainException("The campaign is in a final state and can no longer be modified")
    {
    }
}
