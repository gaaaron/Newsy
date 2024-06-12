using Newsy.Domain.DomainEvents;
using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public abstract class Tag(Guid Id, string Name) : Entity(Id)
{
    public string Name { get; protected set; } = Name;
    public List<Content> Contents { get; set; } = [];

    public abstract bool Attach(Content content);
    public abstract string GetContent();
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
        tag.RaiseDomainEvent(new TagUpdatedEvent(tag.Id, tag.Name));
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

    public void Update(string Name)
    {
        this.Name = Name;
    }

    public override string GetContent() => string.Empty;
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
        tag.RaiseDomainEvent(new TagUpdatedEvent(tag.Id, tag.Name));
        return tag;
    }

    public void Update(string Name, string TextToMatch)
    {
        this.Name = Name;
        this.TextToMatch = TextToMatch;

        RaiseDomainEvent(new TagUpdatedEvent(Id, Name));
    }

    public string TextToMatch { get; protected set; }

    public override bool Attach(Content content)
    {
        if (!content.GetContent().Contains(TextToMatch))
        {
            content.Tags.Remove(this);
            return false;
        }

        content.Tags.Add(this);
        RaiseDomainEvent(new TagsAttachedEvent(Id, content.Id));
        return true;
    }

    public override string GetContent() => TextToMatch;
}
