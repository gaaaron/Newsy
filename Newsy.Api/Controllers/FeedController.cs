using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsy.Application.Feed.Queries.GetContents;

namespace Newsy.Api.Controllers;

[Route("api/[action]")]
[ApiController]
public class FeedController(IMediator sender) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<FeedContentDto>> GetFeedContents([FromQuery]Guid feedId)
    {
        return sender.Send(new GetContentsQuery(feedId));
    }
}
