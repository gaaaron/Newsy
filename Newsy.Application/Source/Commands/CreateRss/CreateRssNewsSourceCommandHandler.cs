using MediatR;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;

namespace Newsy.Application.Source.Commands.CreateRss;

internal sealed class CreateRssNewsSourceCommandHandler(
    INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateRssNewsSourceCommand, CreateRssNewsSourceResponse>
{
    public async Task<CreateRssNewsSourceResponse> Handle(CreateRssNewsSourceCommand request, CancellationToken cancellationToken)
    {
        var source = systemRepository.GetRssSource(request.RssUrl);
        if (source is not null)
            return new CreateRssNewsSourceResponse(source.Id);

        var folder = systemRepository.GetSourceFolder(request.FolderId);
        Guard.NotFound(folder);

        var sourceId = folder!.AddRssSource(request.Name, request.RssUrl);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateRssNewsSourceResponse(sourceId);
    }
}
