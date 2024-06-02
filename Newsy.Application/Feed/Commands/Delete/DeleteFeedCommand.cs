using MediatR;

namespace Newsy.Application.Feed.Commands.Delete;

public sealed record DeleteFeedCommand(Guid Id) : IRequest;
