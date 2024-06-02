using Newsy.Domain.DomainEvents;
using Newsy.Domain.ValueObjects;

namespace Newsy.Domain.Entities;

public class RssSource(Guid Id, string Name, Guid SourceFolderId, RssUrl RssUrl, DateTime? lastScraped) :
    Source(Id, Name, SourceFolderId, lastScraped)
{
    public RssUrl RssUrl { get; set; } = RssUrl;

    public void AddContent(IEnumerable<RssScrapeData> items)
    {
        var ids = new List<Guid>();
        foreach (var item in items)
        {
            var content = new RssContent(Guid.NewGuid(), Id, item.Link, item.ExternalId, item.Published, item.Title, item.Description);
            Contents.Add(content);

            ids.Add(content.Id);
        }

        LastScraped = DateTime.Now;
        RaiseDomainEvent(new ContentCreatedEvent(Id, ids));
    }
}

public record RssScrapeData(string ExternalId, string Title, string Description, string Link, DateTime Published);
