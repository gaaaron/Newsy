using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;

namespace Newsy.Infrastructure.Data.Repositories;

internal class NewsySystemRepository(ApplicationDbContext applicationDbContext) : INewsySystemRepository
{
    public RssSource? GetRssSource(string rssUrl)
    {
        return applicationDbContext.Set<RssSource>().FirstOrDefault(x => x.RssUrl.Value == rssUrl);
    }

    public FacebookSource? GetFacebookSource(string facebookUrl)
    {
        return applicationDbContext.Set<FacebookSource>().FirstOrDefault(x => x.FacebookUrl.Value == facebookUrl);
    }
    public Source? GetSource(Guid sourceId)
    {
        return applicationDbContext.Sources.FirstOrDefault(x => x.Id == sourceId);
    }

    public void InsertSourceTag(SourceTag tag)
    {
        applicationDbContext.Tags.Add(tag);
    }

    public void Insert(Source newsSource)
    {
        applicationDbContext.Sources.Add(newsSource);
    }

    public RssSource? GetRssSourceById(Guid sourceId)
    {
        return applicationDbContext.Set<RssSource>().FirstOrDefault(x => x.Id == sourceId);
    }

    public FacebookSource? GetFacebookSourceById(Guid sourceId)
    {
        return applicationDbContext.Set<FacebookSource>().FirstOrDefault(x => x.Id == sourceId);
    }

    public IEnumerable<Source> GetSourcesBySourceId(Guid sourceId)
    {
        return applicationDbContext.Sources.Where(x => x.Id == sourceId);
    }

    public void InsertContents(IEnumerable<Content> content)
    {
        applicationDbContext.Contents.AddRange(content);
    }

    public IEnumerable<Content> GetContentsByIds(List<Guid> contentIds)
    {
        var contents = applicationDbContext.Contents
                                           .Where(x => contentIds.Contains(x.Id));
        return contents;
    }

    public SourceTag? GetSourceTagBySourceId(Guid sourceId)
    {
        return applicationDbContext.Tags.OfType<SourceTag>().FirstOrDefault(x => x.SourceId == sourceId);
    }

    public SourceFolder? GetSourceFolder(Guid folderId)
    {
        return applicationDbContext.SourceFolders.FirstOrDefault(x => x.Id == folderId);
    }
}
