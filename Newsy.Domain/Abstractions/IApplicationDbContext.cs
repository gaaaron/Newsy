using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newsy.Domain.Entities;

namespace Newsy.Domain.Abstractions;
public interface IApplicationDbContext
{
    public DbSet<Source> Sources { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<SourceFolder> SourceFolders { get; set; }

    public DatabaseFacade Database { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
