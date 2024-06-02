using MediatR;

namespace Newsy.Application.Source.Queries.GetAll;
public record GetAllNewsSourceQuery() : IStreamRequest<NewsSourceDto>;
public record NewsSourceDto(Guid Id, Guid FolderId, string Name, string Type, string Content, Guid SourceId);
