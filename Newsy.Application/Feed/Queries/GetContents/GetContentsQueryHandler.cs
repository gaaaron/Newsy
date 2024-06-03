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
            SELECT STRING_AGG(c_t.Name, ',') AS Tags
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
	            (r.Discriminator = 'IncludeTagRule' and c.Id = ct.ContentsId) or 
	            (r.Discriminator = 'ExcludeTagRule' and c.Id = ct.ContentsId)
            LEFT JOIN [NewsyDb].[dbo].[ContentTags] c_ct ON c_ct.ContentsId = c.Id
            LEFT JOIN [NewsyDb].[dbo].[Tags] c_t ON c_t.Id = c_ct.TagsId
            WHERE f.Id = CONVERT(uniqueidentifier, {request.FeedId})
            GROUP BY c.Id
            HAVING MAX(CASE WHEN r.Discriminator = 'ExcludeTagRule' THEN 1 ELSE 0 END) = 0
            ORDER BY MAX(c.Published) DESC").ToListAsync(cancellationToken: cancellationToken);

        return new GetContentsQueryResponse(feed!.Name, feedContents);
    }
}
