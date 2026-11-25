namespace DonationHub.Domain.Common.Results.Abstractions
{
    public sealed class Result<T> : Result
    {
        public T Value { get; }
        private Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }
        public static Result<T> Success(T Value) => new Result<T>(Value, true, Error.None);
        public static new Result<T> Failure(Error error) => new Result<T>(default!, false, error);
    }
}
