using DonationHub.Domain.Exceptions;
using DonationHub.Domain.Primitives;

namespace DonationHub.Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public const int CurrencyLength = 3;
        public string Currency { get; }
        public decimal Amount { get; }
        private Money()
        {
            Currency = default!;
        }
        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public static Money Create(decimal amount, string currency)
        {
            if (amount < 0)
                throw new Money_Amount_Exception();
            currency = currency?.Trim().ToUpperInvariant() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(currency) || !currency.All(Char.IsLetter) || currency.Length != CurrencyLength) throw new Money_CurrencyException();
            return new Money(Math.Round(amount, 2), currency);
        }
        public static Money Zero(string currency) => Create(0m, currency);
        public Money Add(Money value)
        {
            EnsureThatCurrencyFromSameType(value);
            return Create(Amount + value.Amount, Currency);
        }

        private void EnsureThatCurrencyFromSameType(Money other)
        {
            if (Currency != other.Currency) throw new Money_CurrencyException();
        }
        public override string ToString() => $"{Amount:0.00} {Currency}";

        public override IEnumerable<object> GetEqualityValues()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
