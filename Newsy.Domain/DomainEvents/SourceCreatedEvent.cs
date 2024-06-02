using Newsy.Domain.Abstractions;

namespace Newsy.Domain.DomainEvents;

public record SourceCreatedEvent(Guid SourceId, string Name) : IDomainEvent;
