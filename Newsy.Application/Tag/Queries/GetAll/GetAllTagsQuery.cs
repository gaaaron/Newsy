using MediatR;

namespace Newsy.Application.Tag.Queries.GetAll;
public record GetAllTagsQuery() : IStreamRequest<TagDto>;
public record TagDto(Guid Id, string Name, string Type);
