namespace DonationHub.Domain.Exceptions
{
    public sealed class CampaignHasDonationsException() : DomainException("A campaign with donations can't be hard-deleted. Use soft delete!")
    {
    }
}
