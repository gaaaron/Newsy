using Newsy.Domain.Abstractions;

namespace Newsy.Domain.DomainEvents;

public record TagsAttachedEvent(Guid TagId, Guid ContentId) : IDomainEvent;
