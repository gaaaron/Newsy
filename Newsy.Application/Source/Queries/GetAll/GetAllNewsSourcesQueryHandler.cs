using MediatR;
using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;
using System.Runtime.CompilerServices;

namespace Newsy.Application.Source.Queries.GetAll;

internal class GetAllNewsSourceQueryHandler(IApplicationDbContext applicationDbContext) : IStreamRequestHandler<GetAllNewsSourceQuery, NewsSourceDto>
{
    public async IAsyncEnumerable<NewsSourceDto> Handle(GetAllNewsSourceQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var sources = applicationDbContext.Sources.AsAsyncEnumerable();
        await foreach (var source in sources)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            if (source is RssSource rss)
            {
                yield return new NewsSourceDto(source.Id, source.SourceFolderId, source.Name, 
                    nameof(RssSource), rss.RssUrl.Value, rss.Id);
            }
            if (source is FacebookSource facebook)
            {
                yield return new NewsSourceDto(source.Id, source.SourceFolderId, source.Name, 
                    nameof(FacebookSource), facebook.FacebookUrl.Value, facebook.Id);
            }
        }
    }
}
