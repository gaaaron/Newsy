using MediatR;
using Newsy.Application.Common.Dto;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;

namespace Newsy.Application.Feed.Queries.GetFeedTags;

internal sealed class GetFeedTagsQueryHandler(
    INewsySystemRepository systemRepository) :
    IRequestHandler<GetFeedTagsQuery, IEnumerable<FeedTag>>
{
    public Task<IEnumerable<FeedTag>> Handle(GetFeedTagsQuery request, CancellationToken cancellationToken)
    {
        var feed = systemRepository.GetFeedById(request.FeedId);
        Guard.NotFound(feed);

        var feedTags = new List<FeedTag>();
        var tags = systemRepository.GetAllTags();
        foreach (var tag in tags)
        {
            if (feed!.Rules.OfType<IncludeTagRule>().Any(x => x.TagId == tag.Id))
            {
                feedTags.Add(new FeedTag(tag.Id, tag.Name, FeedTagState.Included));
            }
            else if (feed!.Rules.OfType<ExcludeTagRule>().Any(x => x.TagId == tag.Id))
            {
                feedTags.Add(new FeedTag(tag.Id, tag.Name, FeedTagState.Excluded));
            }
            else
            {
                feedTags.Add(new FeedTag(tag.Id, tag.Name, FeedTagState.None));
            }
        }

        return Task.FromResult(feedTags.AsEnumerable());
    }
}
