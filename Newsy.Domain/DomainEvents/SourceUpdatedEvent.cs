using Newsy.Domain.Abstractions;

namespace Newsy.Domain.DomainEvents;
public record SourceUpdatedEvent(Guid SourceId, string Name) : IDomainEvent;
