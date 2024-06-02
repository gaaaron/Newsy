using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public abstract class FeedRule(Guid Id, Guid FeedId) : Entity(Id)
{
    public Guid FeedId { get; set; } = FeedId;

    public virtual Feed Feed { get; set; } = null!;
}

public abstract class TagRule(Guid Id, Guid FeedId, Guid TagId) : FeedRule(Id, FeedId)
{
    public Guid TagId { get; set; } = TagId;

    public virtual Tag Tag { get; set; } = null!;
}

public abstract class ContentRule(Guid Id, Guid FeedId, Guid ContentId) : FeedRule(Id, FeedId)
{
    public Guid ContentId { get; set; } = ContentId;

    public virtual Content Content { get; set; } = null!;
}

public class IncludeContentRule(Guid Id, Guid FeedId, Guid ContentId) : ContentRule(Id, FeedId, ContentId)
{
}

public class ExcludeContentRule(Guid Id, Guid FeedId, Guid ContentId) : ContentRule(Id, FeedId, ContentId)
{
}

public class IncludeTagRule(Guid Id, Guid FeedId, Guid TagId) : TagRule(Id, FeedId, TagId)
{
}

public class ExcludeTagRule(Guid Id, Guid FeedId, Guid TagId) : TagRule(Id, FeedId, TagId)
{
}
