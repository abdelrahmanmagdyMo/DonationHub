namespace DonationHub.Domain.Exceptions
{
    public sealed class GoalAmountInvalidException() : DomainException("The goal amount must be greater than zero!")
    {
    }
}
