using Newsy.Domain.DomainEvents;
using Newsy.Domain.ValueObjects;

namespace Newsy.Domain.Entities;

public class RssSource : Source
{
    protected RssSource(Guid Id, string Name, Guid SourceFolderId, RssUrl RssUrl, DateTime? lastScraped) : 
        base(Id, Name, SourceFolderId, lastScraped)
    {
        this.RssUrl = RssUrl;
    }

    public static RssSource Create(string Name, Guid SourceFolderId, string RssUrl)
    {
        var source = new RssSource(Guid.NewGuid(), Name, SourceFolderId, 
            ValueObjects.RssUrl.Create(RssUrl), DateTime.Today.AddDays(-1));

        source.RaiseDomainEvent(new SourceCreatedEvent(source.Id, Name));
        return source;
    }

    public RssUrl RssUrl { get; set; }

    public void AddContent(IEnumerable<RssScrapeData> items)
    {
        if (items.Count() == 0)
        {
            return;
        }

        var ids = new List<Guid>();
        foreach (var item in items)
        {
            var content = RssContent.Create(Id, item.Link, item.ExternalId, item.Published, item.Title, item.Description);
            Contents.Add(content);

            ids.Add(content.Id);
        }

        LastScraped = DateTime.Now;
        RaiseDomainEvent(new ContentCreatedEvent(Id, ids));
    }

    public void Update(string name, string rssUrl)
    {
        Name = name;
        RssUrl = RssUrl.Create(rssUrl);
        RaiseDomainEvent(new SourceUpdatedEvent(Id, Name));
    }

}

public record RssScrapeData(string ExternalId, string Title, string Description, string Link, DateTime Published);
