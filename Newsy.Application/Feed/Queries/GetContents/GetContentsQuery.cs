using MediatR;

namespace Newsy.Application.Feed.Queries.GetContents;
public record GetContentsQuery(Guid FeedId) : IRequest<IEnumerable<FeedContentDto>>;
public record FeedContentDto(DateTime Published, string ConcreteSource, string Tags, string ContentType, string Title, string Description);
