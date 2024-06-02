using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newsy.Domain.Primitives;

namespace Newsy.Infrastructure.Data.Interceptors;

// https://www.milanjovanovic.tech/blog/how-to-use-domain-events-to-build-loosely-coupled-systems
internal class DispatchDomainEventsInterceptor : SaveChangesInterceptor
{
    private readonly IPublisher _publisher;

    public DispatchDomainEventsInterceptor(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        PublishDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);

    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await PublishDomainEventsAsync(eventData.Context);

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task PublishDomainEventsAsync(DbContext? context)
    {
        if (context == null) return;

        var domainEvents = context.ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent);
        }
    }
}
