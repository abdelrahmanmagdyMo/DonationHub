namespace DonationHub.Domain.Exceptions
{
    public sealed class CampaignFinalStateCannotChange() : DomainException("A completed or expired campaign can't change")
    {
    }
}
