using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsy.Application.Source.Commands.CreateRss;
using Newsy.Application.Source.Queries.Get;
using Newsy.Application.Source.Queries.GetAll;
using Newsy.Application.Source.Queries.GetHierarchy;

namespace Newsy.Api.Controllers;

[Route("api/[action]")]
[ApiController]
public class NewsSourceController(IMediator sender) : ControllerBase
{
    [HttpPost]
    public Task<CreateRssNewsSourceResponse> CreateRssNewsSource(CreateRssNewsSourceCommand command)
    {
        return sender.Send(command);
    }

    [HttpGet]
    public Task<NewsSourceDetailsDto> GetNewsSource([FromQuery] Guid newsSourceId)
    {
        return sender.Send(new GetNewsSourceQuery(newsSourceId));
    }

    [HttpGet]
    public IAsyncEnumerable<NewsSourceDto> GetAllNewsSource()
    {
        return sender.CreateStream(new GetAllNewsSourceQuery());
    }

    [HttpGet]
    public Task<NewsSourceHierarchy> GetNewsSourceHierarchy()
    {
        return sender.Send(new GetNewsSourceHierarchyQuery());
    }
}
