using MediatR;
using Microsoft.EntityFrameworkCore;
using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;

namespace Newsy.Application.Source.Queries.GetHierarchy;
internal class GetNewsSourceHierarchyQueryHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<GetNewsSourceHierarchyQuery, NewsSourceHierarchy>
{
    public async Task<NewsSourceHierarchy> Handle(GetNewsSourceHierarchyQuery request, CancellationToken cancellationToken)
    {
        var folders = await applicationDbContext.SourceFolders.ToListAsync(cancellationToken: cancellationToken);
        var sources = await applicationDbContext.Sources.ToListAsync(cancellationToken: cancellationToken);
        var root = folders.First(x => x.ParentId == null);

        var hierarchy = ConvertToHierarchy(root, folders, sources);
        return hierarchy;
    }

    private List<NewsSourceHierarchy> GetChildren(Guid id, List<SourceFolder> folders, 
        List<Domain.Entities.Source> sources)
    {
        var childFolders = folders.Where(x => x.ParentId == id).Select(x => ConvertToHierarchy(x, folders, sources));
        var childSources = sources.Where(x => x.SourceFolderId == id).Select(ConvertToHierarchy);

        return childFolders.Union(childSources).OrderBy(x => x.Name).ToList();
    }

    private NewsSourceHierarchy ConvertToHierarchy(SourceFolder folder, List<SourceFolder> folders, 
        List<Domain.Entities.Source> sources)
    {
        return new NewsSourceHierarchy
        {
            Id = folder.Id,
            Name = folder.Name,
            Type = HierarchyType.Folder,
            Children = GetChildren(folder.Id, folders, sources)
        };
    }

    private NewsSourceHierarchy ConvertToHierarchy(Domain.Entities.Source source)
    {
        return new NewsSourceHierarchy
        {
            Id = source.Id,
            Name = source.Name,
            Type = source is RssSource ?
                    HierarchyType.RssNewsSource :
                   source is FacebookSource ?
                    HierarchyType.FacebookNewsSource :
                    HierarchyType.Unknown,
            Children = null
        };
    }
}
