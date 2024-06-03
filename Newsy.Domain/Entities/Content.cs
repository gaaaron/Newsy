using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public abstract class Content(Guid Id, Guid? SourceId, string ConcreteSource, string ExternalId, DateTime Published) : Entity(Id)
{
    public Guid? SourceId { get; set; } = SourceId;
    public string ConcreteSource { get; set; } = ConcreteSource;
    public string ExternalId { get; set; } = ExternalId;
    public DateTime Published { get; set; } = Published;
    public virtual Source? Source { get; set; }
    public List<Tag> Tags { get; set; } = [];
    public abstract string GetContent();
}

public class RssContent : Content
{
    protected RssContent(Guid Id, Guid? SourceId, string ConcreteSource, string ExternalId,
        DateTime Published, string Title, string Description) : base(Id, SourceId, ConcreteSource, ExternalId, Published)
    {
        this.Title = Title;
        this.Description = Description;
    }

    public static RssContent Create(Guid? SourceId, string ConcreteSource, string ExternalId,
        DateTime Published, string Title, string Description)
    {
        return new RssContent(Guid.NewGuid(), SourceId, ConcreteSource, ExternalId, Published, Title, Description);
    }

    public string Title { get; set; }
    public string Description { get; set; }

    public override string GetContent()
    {
        return $"{Title}{Environment.NewLine}{Description}";
    }
}
