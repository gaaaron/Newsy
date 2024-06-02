using MediatR;

namespace Newsy.Application.Source.Commands.ScrapeRss;
public record ScrapeRssCommand(Guid SourceId) : IRequest<string>;
