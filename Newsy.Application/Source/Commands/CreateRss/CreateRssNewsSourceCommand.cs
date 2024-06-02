using MediatR;

namespace Newsy.Application.Source.Commands.CreateRss;

public sealed record CreateRssNewsSourceCommand(Guid FolderId, string Name, string RssUrl) : IRequest<CreateRssNewsSourceResponse>;
public record CreateRssNewsSourceResponse(Guid SourceId);
