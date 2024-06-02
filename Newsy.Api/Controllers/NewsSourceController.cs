using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsy.Application.Source.Commands.CreateOrEditRss;
using Newsy.Application.Source.Commands.Delete;
using Newsy.Application.Source.Commands.ScrapeRss;
using Newsy.Application.Source.Queries.Get;
using Newsy.Application.Source.Queries.GetAll;
using Newsy.Application.Source.Queries.GetHierarchy;

namespace Newsy.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class NewsSourceController(IMediator sender) : ControllerBase
{
    [HttpGet("{newsSourceId}")]
    public Task<NewsSourceDetailsDto> Get(Guid newsSourceId)
    {
        return sender.Send(new GetNewsSourceQuery(newsSourceId));
    }

    [HttpGet]
    public IAsyncEnumerable<NewsSourceDto> GetAll()
    {
        return sender.CreateStream(new GetAllNewsSourceQuery());
    }

    [HttpGet]
    public Task<NewsSourceHierarchy> GetHierarchy()
    {
        return sender.Send(new GetNewsSourceHierarchyQuery());
    }

    [HttpPost]
    public Task<CreateOrEditRssResponse> CreateOrdEditRss(CreateOrEditRssCommand command)
    {
        return sender.Send(command);
    }

    [HttpPost]
    public Task<string> ScrapeRss(ScrapeRssCommand command)
    {
        return sender.Send(command);
    }

    [HttpDelete("{newsSourceId}")]
    public Task Delete(Guid newsSourceId)
    {
        return sender.Send(new DeleteSourceCommand(newsSourceId));
    }
}
