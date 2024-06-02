using MediatR;
using Newsy.Domain.Abstractions;
using System.Runtime.CompilerServices;


namespace Newsy.Application.Feed.Queries.GetAll;

internal class GetAllQueryHandler(IApplicationDbContext applicationDbContext) : IStreamRequestHandler<GetAllQuery, FeedDto>
{
    public async IAsyncEnumerable<FeedDto> Handle(GetAllQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var feeds = applicationDbContext.Feeds.AsAsyncEnumerable();
        await foreach (var feed in feeds)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            yield return new FeedDto(feed.Id, feed.Name);
        }
    }
}
