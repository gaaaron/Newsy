using MediatR;

namespace Newsy.Application.Feed.Commands.Create;

public sealed record CreateFeedCommand(string Name) : IRequest<CreateFeedResponse>;
public record CreateFeedResponse(Guid FeedId);
