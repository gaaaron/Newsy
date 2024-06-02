using MediatR;
using Newsy.Application.Common.Dto;

namespace Newsy.Application.Feed.Commands.UpdateTags;
public sealed record UpdateFeedTagsCommand(Guid FeedId, List<FeedTag> FeedTags) : IRequest;
