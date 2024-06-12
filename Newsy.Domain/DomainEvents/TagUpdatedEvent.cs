using Newsy.Domain.Abstractions;

namespace Newsy.Domain.DomainEvents;

public record TagUpdatedEvent(Guid TagId, string Name) : IDomainEvent;
