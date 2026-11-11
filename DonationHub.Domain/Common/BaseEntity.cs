namespace DonationHub.Domain.Common
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public Guid Id { get; }
        protected BaseEntity(Guid id) => Id = id == Guid.Empty ? Guid.NewGuid() : id;
        public override bool Equals(object? obj) => Equals(obj as BaseEntity);

        public override int GetHashCode() => HashCode.Combine(GetType(), Id);

        public bool Equals(BaseEntity? other)
        {
            if (other is null) return false;
            if (other.GetType() != GetType()) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }
        public static bool operator ==(BaseEntity? a, BaseEntity? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }
        public static bool operator !=(BaseEntity? a, BaseEntity? b) => !(a == b);
    }
}
