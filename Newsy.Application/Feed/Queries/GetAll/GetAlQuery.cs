using MediatR;

namespace Newsy.Application.Feed.Queries.GetAll;
public record GetAllQuery() : IStreamRequest<FeedDto>;
public record FeedDto(Guid Id, string Name);
