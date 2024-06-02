using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public abstract class Content(Guid Id, Guid SourceId, string ConcreteSource, string ExternalId, DateTime Published) : Entity(Id)
{
    public Guid SourceId { get; set; } = SourceId;
    public string ConcreteSource { get; set; } = ConcreteSource;
    public string ExternalId { get; set; } = ExternalId;
    public DateTime Published { get; set; } = Published;
    public virtual Source Source { get; set; } = null!;
    public List<Tag> Tags { get; set; } = [];
    public abstract string GetContent();
}

public class RssContent(Guid Id, Guid SourceId, string ConcreteSource, string ExternalId, 
    DateTime Published, string Title, string Description) :
    Content(Id, SourceId, ConcreteSource, ExternalId, Published)
{
    public string Title { get; set; } = Title;
    public string Description { get; set; } = Description;

    public override string GetContent()
    {
        return $"{Title}{Environment.NewLine}{Description}";
    }
}
