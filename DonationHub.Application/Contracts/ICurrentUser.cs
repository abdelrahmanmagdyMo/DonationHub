namespace DonationHub.Application.Contracts
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
    }
}
