using MediatR;
using Microsoft.EntityFrameworkCore;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;

namespace Newsy.Application.Source.Queries.Get;
internal class GetNewsSourceQueryHandler(IApplicationDbContext applicationDbContext) : IRequestHandler<GetNewsSourceQuery, NewsSourceDetailsDto>
{
    public async Task<NewsSourceDetailsDto> Handle(GetNewsSourceQuery request, CancellationToken cancellationToken)
    {
        var source = await applicationDbContext.Sources.FirstOrDefaultAsync(x => x.Id == request.NewsSourceId);
        Guard.NotFound(source, request.NewsSourceId);

        if (source is RssSource rss)
        {
            return new NewsSourceDetailsDto(source.Id, source.SourceFolderId, source.Name, 
                nameof(RssSource), rss.RssUrl.Value, rss.Id);
        }
        if (source is FacebookSource facebook)
        {
            return new NewsSourceDetailsDto(source.Id, source.SourceFolderId, source.Name, 
                nameof(FacebookSource), facebook.FacebookUrl.Value, facebook.Id);
        }

        throw new NotSupportedException($"{nameof(source)} source type is not supported!");
    }
}
