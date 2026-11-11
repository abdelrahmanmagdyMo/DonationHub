namespace DonationHub.Domain.Exceptions
{
    public sealed class CampaignEndedExceptionException() : DomainException("The campaign has ended!")
    {
    }
}
