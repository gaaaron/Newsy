using Newsy.Domain.DomainEvents;
using Newsy.Domain.Primitives;
using Newsy.Domain.ValueObjects;

namespace Newsy.Domain.Entities;

public abstract class Source : Entity
{
    public Source(Guid id, string name, Guid sourceFolderId, DateTime? lastScraped) : base(id)
    {
        Id = id;
        Name = name;
        LastScraped = lastScraped;
        SourceFolderId = sourceFolderId;
    }

    public string Name { get; set; }
    public DateTime? LastScraped { get; set; }

    public List<Content> Contents { get; set; } = [];

    public Guid SourceFolderId { get; set; }
    public virtual SourceFolder SourceFolder { get; set; } = null!;
}

public class FacebookSource : Source
{
    protected FacebookSource(Guid Id, string Name, Guid SourceFolderId, FacebookUrl FacebookUrl, DateTime? lastScraped) : 
        base(Id, Name, SourceFolderId, lastScraped)
    {
        this.FacebookUrl = FacebookUrl;
    }

    public static FacebookSource Create(string Name, Guid SourceFolderId, string FacebookUrl)
    {
        var source = new FacebookSource(Guid.NewGuid(), Name, SourceFolderId, 
            ValueObjects.FacebookUrl.Create(FacebookUrl), DateTime.Today.AddDays(-1));

        source.RaiseDomainEvent(new SourceCreatedEvent(source.Id, Name));
        return source;
    }

    public FacebookUrl FacebookUrl { get; set; }
}
