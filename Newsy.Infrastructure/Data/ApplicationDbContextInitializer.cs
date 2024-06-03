using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsy.Domain.Entities;
using Newsy.Domain.ValueObjects;

namespace Newsy.Infrastructure.Data;

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

internal class ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
{

    public async Task InitialiseAsync()
    {
        try
        {
            context.Database.EnsureCreated();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!context.Sources.Any())
        {
            var sourceFolder = SourceFolder.Create(null, "Külföldi oldalak");
            context.Set<SourceFolder>().Add(sourceFolder);

            var bbcRss = RssSource.Create("BBC", sourceFolder.Id, "https://feeds.bbci.co.uk/news/rss.xml");
            context.Sources.Add(bbcRss);

            var feed = Feed.Create("Hírek");
            context.Feeds.Add(feed);

            await context.SaveChangesAsync();
        }
    }
}
