using MediatR;

namespace Newsy.Application.Source.Queries.GetHierarchy;
public record GetNewsSourceHierarchyQuery : IRequest<NewsSourceHierarchy>;

public record NewsSourceHierarchy
{
    public Guid Id { get; set; }
    public HierarchyType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<NewsSourceHierarchy>? Children { get; set; }
}

public enum HierarchyType
{
    Folder,
    RssNewsSource,
    FacebookNewsSource,
    Unknown
}
