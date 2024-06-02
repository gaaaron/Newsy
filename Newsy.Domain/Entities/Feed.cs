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

    public void ExcludeTag(Guid tagId)
    {
        var rule = new IncludeTagRule(Guid.NewGuid(), Id, tagId);
        Rules.Add(rule);
    }

    public void UpdateRules(List<Guid> includeTags, List<Guid> excludeTags)
    {
        var oldIncludeTags = Rules.OfType<IncludeTagRule>().ToList();
        var oldExcludeTags = Rules.OfType<ExcludeTagRule>().ToList();

        var toRemove1 = oldIncludeTags.Where(x => !includeTags.Contains(x.Id));
        var toRemove2 = oldExcludeTags.Where(y => !excludeTags.Contains(y.Id));

        var toAdd1 = includeTags.Where(x => !oldIncludeTags.Select(t => t.Id).Contains(x))
                                .Select(x => new IncludeTagRule(Guid.NewGuid(), Id, x));

        var toAdd2 = excludeTags.Where(x => !oldExcludeTags.Select(t => t.Id).Contains(x))
                                .Select(x => new ExcludeTagRule(Guid.NewGuid(), Id, x));

        Rules.RemoveAll(x => toRemove1.Contains(x) || toRemove2.Contains(x));

        foreach (var item in toAdd1)
        {
            Rules.Add(item);
        }

        foreach (var item in toAdd2)
        {
            Rules.Add(item);
        }
    }
}
