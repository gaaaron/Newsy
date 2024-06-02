using MediatR;

namespace Newsy.Application.Source.Queries.Get;
public record GetNewsSourceQuery(Guid NewsSourceId) : IRequest<NewsSourceDetailsDto>;
public record NewsSourceDetailsDto(Guid Id, Guid FolderId, string Name, string Type, string Content, Guid SourceId);
