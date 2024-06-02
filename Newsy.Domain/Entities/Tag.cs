using Newsy.Domain.DomainEvents;
using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public abstract class Tag(Guid Id, string Name) : Entity(Id)
{
    public string Name { get; set; } = Name;
    public List<Content> Contents { get; set; } = [];

    public abstract bool Attach(Content content);
}

public class SourceTag(Guid Id, string Name, Guid SourceId) : Tag(Id, Name)
{
    public Guid SourceId { get; set; } = SourceId;
    public virtual Source Source { get; set; } = null!;

    public override bool Attach(Content content)
    {
        if (content.SourceId != SourceId)
            return false;

        content.Tags.Add(this);
        RaiseDomainEvent(new TagsAttachedEvent(Id, content.Id));
        return true;
    }
}
