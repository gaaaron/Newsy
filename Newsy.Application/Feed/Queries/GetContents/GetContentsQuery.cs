using MediatR;

namespace Newsy.Application.Feed.Queries.GetContents;
public record GetContentsQuery(Guid FeedId) : IRequest<GetContentsQueryResponse>;

public record GetContentsQueryResponse(string FeedName, IEnumerable<FeedContentDto> Contents);
public record FeedContentDto(DateTime Published, string ConcreteSource, string Tags, string ContentType, string Title, string Description);
