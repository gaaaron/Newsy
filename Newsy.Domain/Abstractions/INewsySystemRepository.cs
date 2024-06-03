using Newsy.Domain.Entities;

namespace Newsy.Domain.Abstractions;

public interface INewsySystemRepository
{
    // SOURCE
    RssSource? GetRssSourceByUrl(string rssUrl);
    RssSource? GetRssSourceById(Guid sourceId);
    void DeleteSource(Guid id);
    SourceFolder? GetDefaultFolder();

    // CONTENT
    IEnumerable<Content> GetContentsByIds(List<Guid> contentIds);
    IEnumerable<Content> GetAllContents();

    // TAG
    IEnumerable<Tag> GetAllTags();
    Tag? GetTag(Guid tagId);
    SourceTag? GetSourceTagBySourceId(Guid sourceId);
    void InsertTag(Tag tag);

    // FEED
    Feed? GetFeedByName(string name);
    Feed? GetFeedById(Guid feedId);
    void InsertFeed(Feed feed);
    void DeleteFeed(Guid feedId);
}
