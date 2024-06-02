using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public class Feed(Guid Id, string Name) : Entity(Id)
{
    public string Name { get; set; } = Name;
    public List<FeedRule> Rules { get; set; } = [];

    public void IncludeTag(Guid tagId)
    {
        var rule = new IncludeTagRule(Guid.NewGuid(), Id, tagId);
        Rules.Add(rule);
    }
}
