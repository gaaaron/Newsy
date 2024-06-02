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

    public string Name { get; init; }
    public DateTime? LastScraped { get; set; }

    public List<Content> Contents { get; set; } = [];

    public Guid SourceFolderId { get; set; }
    public virtual SourceFolder SourceFolder { get; set; } = null!;
}

public class FacebookSource(Guid Id, string Name, Guid SourceFolderId, FacebookUrl FacebookUrl, DateTime? lastScraped) :
    Source(Id, Name, SourceFolderId, lastScraped)
{
    public FacebookUrl FacebookUrl { get; set; } = FacebookUrl;
}
