namespace DonationHub.Domain.ProcessedStripeEvent
{
    public sealed class ProcessedStripeEvent
    {
        public const int EventIdMaxLength = 255;
        public const int TypeMaxLength = 100;
        public string EventId { get; private set; } = default!;
        public string Type { get; private set; } = default!;
        public DateTimeOffset ProcessedAt { get; private set; }
        private ProcessedStripeEvent() { }
        public ProcessedStripeEvent(string eventId, string type, DateTimeOffset processedAt)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(eventId);
            ArgumentException.ThrowIfNullOrWhiteSpace(type);
            eventId = eventId.Trim();
            type = type.Trim();
            if (eventId.Length > EventIdMaxLength) throw new ArgumentException("Event Id Cannot Exceed 255 letters");
            if (type.Length > TypeMaxLength) throw new ArgumentException("Type Cannot Exceed 100 letters");
            EventId = eventId;
            Type = type;
            ProcessedAt = processedAt;
        }


    }
}
