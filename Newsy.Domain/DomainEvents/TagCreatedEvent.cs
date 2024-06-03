using Newsy.Domain.Abstractions;

namespace Newsy.Domain.DomainEvents;

public record TagCreatedEvent(Guid TagId, string Name) : IDomainEvent;
