using Newsy.Domain.Abstractions;

namespace Newsy.Domain.DomainEvents;

public record ContentCreatedEvent(Guid SourceId, List<Guid> ContentIds) : IDomainEvent;
