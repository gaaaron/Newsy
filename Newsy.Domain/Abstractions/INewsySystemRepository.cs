using Newsy.Domain.Entities;

namespace Newsy.Domain.Abstractions;

public interface INewsySystemRepository
{
    RssSource? GetRssSource(string rssUrl);
    RssSource? GetRssSourceById(Guid sourceId);
    Source? GetSource(Guid sourceId);

    FacebookSource? GetFacebookSource(string facebookUrl);
    FacebookSource? GetFacebookSourceById(Guid sourceId);
    IEnumerable<Source> GetSourcesBySourceId(Guid sourceId);
    void Insert(Source newsSource);
    void InsertContents(IEnumerable<Content> content);
    IEnumerable<Content> GetContentsByIds(List<Guid> contentIds);
    void InsertSourceTag(SourceTag tag);
    SourceTag? GetSourceTagBySourceId(Guid sourceId);
    SourceFolder? GetSourceFolder(Guid folderId);
}
