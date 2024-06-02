using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsy.Application.Source.Commands.ScrapeRss;

namespace Newsy.Api.Controllers;

[Route("api/[action]")]
[ApiController]
public class AdminController(IMediator sender) : ControllerBase
{
    [HttpPost]
    public Task<string> ScrapeRss(ScrapeRssCommand command)
    {
        return sender.Send(command);
    }
}
