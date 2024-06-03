using Newsy.Domain.DomainEvents;
using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public abstract class Tag(Guid Id, string Name) : Entity(Id)
{
    public string Name { get; set; } = Name;
    public List<Content> Contents { get; set; } = [];

    public abstract bool Attach(Content content);
}

public class SourceTag : Tag
{
    protected SourceTag(Guid Id, string Name, Guid SourceId) : base(Id, Name)
    {
        this.SourceId = SourceId;
    }

    public static SourceTag Create(string Name, Guid SourceId)
    {
        var tag = new SourceTag(Guid.NewGuid(), Name, SourceId);
        tag.RaiseDomainEvent(new TagCreatedEvent(tag.Id, tag.Name));
        return tag;
    }

    public Guid SourceId { get; set; }
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

public class ContainsTag : Tag
{
    protected ContainsTag(Guid Id, string Name, string TextToMatch) : base(Id, Name)
    {
        this.TextToMatch = TextToMatch;
    }

    public static ContainsTag Create(string Name, string TextToMatch)
    {
        var tag = new ContainsTag(Guid.NewGuid(), Name, TextToMatch);
        tag.RaiseDomainEvent(new TagCreatedEvent(tag.Id, tag.Name));
        return tag;
    }

    public string TextToMatch { get; set; }

    public override bool Attach(Content content)
    {
        if (!content.GetContent().Contains(TextToMatch))
            return false;

        content.Tags.Add(this);
        RaiseDomainEvent(new TagsAttachedEvent(Id, content.Id));
        return true;
    }
}
