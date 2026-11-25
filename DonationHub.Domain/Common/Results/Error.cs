namespace DonationHub.Domain.Common.Results
{
    public sealed record Error(string Code, string? Message = null, ErrorType? ErrorType = null)
    {
        public static Error None = new(string.Empty, null, null);
        public static Error Validation(string code, string message)
            => new Error(code, message, Results.ErrorType.Validation);
    }

}
