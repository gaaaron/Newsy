using Newsy.Domain.Primitives;

namespace Newsy.Domain.Entities;
public class SourceFolder : Entity
{
    protected SourceFolder(Guid Id, Guid? ParentId, string Name) : base(Id)
    {
        this.ParentId = ParentId;
        this.Name = Name;
    }

    public static SourceFolder Create(Guid? ParentId, string Name)
    {
        return new SourceFolder(Guid.NewGuid(), ParentId, Name);
    }

    public Guid? ParentId { get; set; }
    public string Name { get; set; }

    public List<Source> Sources { get; set; } = [];
    public virtual SourceFolder? Parent { get; set; } = null!;

    public Guid AddFacebookSource(string Name, string FacebookUrl)
    {
        var source = FacebookSource.Create(Name, Id, FacebookUrl);
        Sources.Add(source);
        return source.Id;
    }

    public Guid AddRssSource(string Name, string RssUrl)
    {
        var source = RssSource.Create(Name, Id, RssUrl);
        Sources.Add(source);
        return source.Id;
    }
}
