using MediatR;
using Microsoft.EntityFrameworkCore;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;

namespace Newsy.Application.Feed.Queries.GetContents;
internal class GetContentsQueryHandler(IApplicationDbContext applicationDbContext, INewsySystemRepository newsySystemRepository) : 
    IRequestHandler<GetContentsQuery, GetContentsQueryResponse>
{
    public async Task<GetContentsQueryResponse> Handle(GetContentsQuery request, CancellationToken cancellationToken)
    {
        var feed = newsySystemRepository.GetFeedById(request.FeedId);
        Guard.NotFound(feed);

        var feedContents = await applicationDbContext.Database.SqlQuery<FeedContentDto>($@"
            SELECT STRING_AGG(t.Name, ',') AS Tags
	              ,MAX(c.Published) AS Published
	              ,MAX(c.ConcreteSource) AS ConcreteSource
	              ,MAX(c.Discriminator) AS ContentType
	              ,MAX(c.Title) AS Title
	              ,MAX(c.Description) AS Description
            FROM [NewsyDb].[dbo].[Feeds] f
            JOIN [NewsyDb].[dbo].[FeedRules] r ON r.FeedId = f.Id
            LEFT JOIN [NewsyDb].[dbo].[Tags] t ON t.Id = r.TagId
            LEFT JOIN [NewsyDb].[dbo].[ContentTags] ct ON ct.TagsId = t.Id
            JOIN [NewsyDb].[dbo].[Contents] c ON
	            ((r.Discriminator = 'IncludeTagRule' and c.Id = ct.ContentsId)) and not 
	            ((r.Discriminator = 'ExcludeTagRule' and c.Id = ct.ContentsId))
            WHERE f.Id = CONVERT(uniqueidentifier, {request.FeedId})
            GROUP BY c.Id
            ORDER BY MAX(c.Published) DESC").ToListAsync(cancellationToken: cancellationToken);

        return new GetContentsQueryResponse(feed!.Name, feedContents);
    }
}
