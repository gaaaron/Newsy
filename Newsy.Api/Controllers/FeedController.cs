using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsy.Application.Common.Dto;
using Newsy.Application.Feed.Commands.Create;
using Newsy.Application.Feed.Commands.Delete;
using Newsy.Application.Feed.Commands.UpdateTags;
using Newsy.Application.Feed.Queries.GetAll;
using Newsy.Application.Feed.Queries.GetContents;
using Newsy.Application.Feed.Queries.GetFeedTags;

namespace Newsy.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class FeedController(IMediator sender) : ControllerBase
{
    [HttpGet("{feedId}")]
    public Task<GetContentsQueryResponse> Get(Guid feedId)
    {
        return sender.Send(new GetContentsQuery(feedId));
    }

    [HttpGet]
    public IAsyncEnumerable<FeedDto> GetAll()
    {
        return sender.CreateStream(new GetAllQuery());
    }

    [HttpGet("{feedId}")]
    public Task<IEnumerable<FeedTag>> GetFeedTags(Guid feedId)
    {
        return sender.Send(new GetFeedTagsQuery(feedId));
    }

    [HttpPost]
    public Task<CreateFeedResponse> Create(CreateFeedCommand command)
    {
        return sender.Send(command);
    }

    [HttpPost]
    public Task UpdateFeedTags(UpdateFeedTagsCommand command)
    {
        return sender.Send(command);
    }

    [HttpDelete("{feedId}")]
    public Task Delete(Guid feedId)
    {
        return sender.Send(new DeleteFeedCommand(feedId));
    }
}
