using Microsoft.EntityFrameworkCore;
using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;
using Newsy.Domain.ValueObjects;

namespace Newsy.Infrastructure.Data.Repositories;

internal class NewsySystemRepository(ApplicationDbContext applicationDbContext) : INewsySystemRepository
{
    public RssSource? GetRssSourceByUrl(string rssUrl)
    {
        var url = RssUrl.Create(rssUrl)!;
        return applicationDbContext.Set<RssSource>().FirstOrDefault(x => x.RssUrl == url);
    }

    public RssSource? GetRssSourceById(Guid sourceId)
    {
        return applicationDbContext.Set<RssSource>().FirstOrDefault(x => x.Id == sourceId);
    }

    public void DeleteSource(Guid id)
    {
        var source = applicationDbContext.Sources.Include(x => x.Contents).FirstOrDefault(x => x.Id == id);
        if (source is null)
            return;

        foreach (var content in source.Contents)
        {
            applicationDbContext.Contents.Remove(content);
        }
        applicationDbContext.Sources.Remove(source);

        var tag = applicationDbContext.Tags.OfType<SourceTag>().FirstOrDefault(x => x.SourceId == id);
        if (tag is null)
            return;

        applicationDbContext.Tags.Remove(tag);
    }

    public SourceFolder? GetDefaultFolder()
    {
        return applicationDbContext.SourceFolders.FirstOrDefault();
    }

    public IEnumerable<Content> GetContentsByIds(List<Guid> contentIds)
    {
        var contents = applicationDbContext.Contents
                                           .Where(x => contentIds.Contains(x.Id));
        return contents;
    }

    public IEnumerable<Tag> GetAllTags()
    {
        return applicationDbContext.Tags.ToList();
    }

    public SourceTag? GetSourceTagBySourceId(Guid sourceId)
    {
        return applicationDbContext.Tags.OfType<SourceTag>().FirstOrDefault(x => x.SourceId == sourceId);
    }

    public void InsertSourceTag(SourceTag tag)
    {
        applicationDbContext.Tags.Add(tag);
    }

    public Feed? GetFeedByName(string name)
    {
        return applicationDbContext.Feeds.Include(x => x.Rules).FirstOrDefault(x => x.Name == name);
    }

    public Feed? GetFeedById(Guid feedId)
    {
        return applicationDbContext.Feeds.Include(x => x.Rules).FirstOrDefault(x => x.Id == feedId);
    }

    public void InsertFeed(Feed feed)
    {
        applicationDbContext.Feeds.Add(feed);
    }

    public void DeleteFeed(Guid feedId)
    {
        var feed = applicationDbContext.Feeds.FirstOrDefault(x => x.Id == feedId);
        if (feed is null)
            return;

        feed.Rules.Clear();
        applicationDbContext.Feeds.Remove(feed);
    }
}
