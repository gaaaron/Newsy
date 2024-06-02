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
            var sourceFolder = new SourceFolder(Guid.Parse("D6012FA7-C9EC-490C-BC0F-48E2295D2450"), null, "Külföldi oldalak");
            context.Set<SourceFolder>().Add(sourceFolder);

            var bbcRss = new RssSource(Guid.Parse("01605e68-6670-499d-a01b-b963067a9e36"), "BBC", 
                sourceFolder.Id, RssUrl.Create("https://feeds.bbci.co.uk/news/rss.xml"), DateTime.Today.AddDays(-1));
            context.Sources.Add(bbcRss);

            var tag = new SourceTag(Guid.Parse("130a1e32-d640-43dd-a96c-aace796bc0e0"), "BBC", bbcRss.Id);
            context.Tags.Add(tag);

            var feed = new Feed(Guid.Parse("149725e1-26e7-4e8d-a662-cf27eb6d8767"), "Hírek");
            feed.IncludeTag(tag.Id);
            context.Feeds.Add(feed);

            await context.SaveChangesAsync();
        }
    }
}
