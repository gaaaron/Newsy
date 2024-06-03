using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public class Feed : Entity
{
    protected Feed(Guid Id, string Name) : base(Id)
    {
        this.Name = Name;
    }

    public static Feed Create(string Name)
    {
        return new Feed(Guid.NewGuid(), Name);
    }

    public string Name { get; set; }
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
        var prevIncludeTags = Rules.OfType<IncludeTagRule>().ToList();
        var prevExcludeTags = Rules.OfType<ExcludeTagRule>().ToList();

        var prevInclToRemove = prevIncludeTags.Where(x => !includeTags.Contains(x.Id));
        var prevExclToRemove = prevExcludeTags.Where(y => !excludeTags.Contains(y.Id));

        var inclToAdd = includeTags.Where(x => !prevIncludeTags.Select(t => t.Id).Contains(x))
                                   .Select(x => new IncludeTagRule(Guid.NewGuid(), Id, x));

        var exclToAdd = excludeTags.Where(x => !prevExcludeTags.Select(t => t.Id).Contains(x))
                                   .Select(x => new ExcludeTagRule(Guid.NewGuid(), Id, x));

        Rules.RemoveAll(x => prevInclToRemove.Contains(x) || prevExclToRemove.Contains(x));

        foreach (var item in inclToAdd)
        {
            Rules.Add(item);
        }

        foreach (var item in exclToAdd)
        {
            Rules.Add(item);
        }
    }
}
