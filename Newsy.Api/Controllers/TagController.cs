﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newsy.Application.Tag.Queries.GetAll;

namespace Newsy.Api.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TagController(IMediator sender) : ControllerBase
{
    [HttpGet]
    public IAsyncEnumerable<TagDto> GetAll()
    {
        return sender.CreateStream(new GetAllTagsQuery());
    }
}