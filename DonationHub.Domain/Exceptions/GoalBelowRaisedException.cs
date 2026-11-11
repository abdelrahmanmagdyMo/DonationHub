namespace DonationHub.Domain.Exceptions
{
    public sealed class GoalBelowRaisedException() : DomainException("The goal amount can't be lowered below the amount already raised")
    {
    }
}
