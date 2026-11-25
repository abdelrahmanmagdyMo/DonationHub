using DonationHub.Domain.Common.Results.Abstractions;
using DonationHub.Domain.ProcessedStripeEvents.Errors;

namespace DonationHub.Domain.ProcessedStripeEvents;

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
        eventId = eventId.Trim();
        type = type.Trim();
        EventId = eventId;
        Type = type;
        ProcessedAt = processedAt;
    }
    public static Result<ProcessedStripeEvent> Create(string eventId, string type, TimeProvider processedAt)
    {
        if (eventId is null || string.IsNullOrEmpty(eventId)) return Result<ProcessedStripeEvent>.Failure(ProcessedStripeEventErrors.EventIdIsRequired);
        if (type is null || string.IsNullOrEmpty(type)) return Result<ProcessedStripeEvent>.Failure(ProcessedStripeEventErrors.TypeIsRequired);
        if (eventId.Length > EventIdMaxLength) return Result<ProcessedStripeEvent>.Failure(ProcessedStripeEventErrors.EventIdMaxLength);
        if (type.Length > TypeMaxLength) return Result<ProcessedStripeEvent>.Failure(ProcessedStripeEventErrors.TypeMaxLength);

        var processedStripeEvent = new ProcessedStripeEvent(eventId, type, processedAt.GetUtcNow());
        return Result<ProcessedStripeEvent>.Success(processedStripeEvent);
    }

}
