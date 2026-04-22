namespace ApplicationInfra.Sample;

public sealed record OrderPlacedEvent(Guid OrderId, DateTimeOffset PlacedAt);
