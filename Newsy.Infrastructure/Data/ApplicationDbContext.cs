using Microsoft.EntityFrameworkCore;
using Newsy.Domain.Abstractions;
using System.Reflection;
using Newsy.Domain.Primitives;
using MediatR;
using Newsy.Domain.Entities;

namespace Newsy.Infrastructure.Data
{
    internal class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : 
        DbContext(options), IUnitOfWork, IApplicationDbContext
    {
        public DbSet<Source> Sources { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<SourceFolder> SourceFolders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventsAsync(this);

            return result;
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
                await publisher.Publish(domainEvent);
            }
        }
    }
}
