using MediatR;

namespace Newsy.Application.Source.Commands.CreateOrEditRss;

public sealed record CreateOrEditRssCommand(Guid? Id, string Name, string RssUrl) : IRequest<CreateOrEditRssResponse>;
public record CreateOrEditRssResponse(Guid SourceId);
