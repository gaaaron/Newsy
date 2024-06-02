using MediatR;
using Newsy.Application.Common.Dto;

namespace Newsy.Application.Feed.Queries.GetFeedTags;

public record GetFeedTagsQuery(Guid FeedId) : IRequest<IEnumerable<FeedTag>>;
