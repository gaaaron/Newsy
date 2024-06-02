using Newsy.Domain.DomainEvents;
using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public class SourceFolder(Guid Id, Guid? ParentId, string Name) : Entity(Id)
{
    public Guid? ParentId { get; set; } = ParentId;
    public string Name { get; set; } = Name;

    public List<Source> Sources { get; set; } = [];
    public virtual SourceFolder? Parent { get; set; } = null!;

    public Guid AddFacebookSource(string Name, string FacebookUrl)
    {
        var source = new FacebookSource(Guid.NewGuid(), Name, Id, ValueObjects.FacebookUrl.Create(FacebookUrl), DateTime.Today.AddDays(-1));
        Sources.Add(source);

        RaiseDomainEvent(new SourceCreatedEvent(Id, Name));
        return source.Id;
    }

    public Guid AddRssSource(string Name, string RssUrl)
    {
        var source = new RssSource(Guid.NewGuid(), Name, Id, ValueObjects.RssUrl.Create(RssUrl), DateTime.Today.AddDays(-1));
        Sources.Add(source);

        RaiseDomainEvent(new SourceCreatedEvent(Id, Name));
        return source.Id;
    }
}
