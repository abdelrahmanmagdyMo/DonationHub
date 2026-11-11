namespace DonationHub.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        public abstract IEnumerable<object> GetEqualityValues();
        private bool ValuesAreEqual(ValueObject obj) => GetEqualityValues().SequenceEqual(obj.GetEqualityValues());
        public override bool Equals(object? obj) => obj is ValueObject check && ValuesAreEqual(check);

        public override int GetHashCode() => GetEqualityValues().Aggregate(default(int), HashCode.Combine);

        public bool Equals(ValueObject? other) => other is not null && ValuesAreEqual(other);
    }
}





