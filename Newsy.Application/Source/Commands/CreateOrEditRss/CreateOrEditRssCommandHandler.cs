using MediatR;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;

namespace Newsy.Application.Source.Commands.CreateOrEditRss;

internal sealed class CreateOrEditRssCommandHandler(
    INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateOrEditRssCommand, CreateOrEditRssResponse>
{
    public async Task<CreateOrEditRssResponse> Handle(CreateOrEditRssCommand request, CancellationToken cancellationToken)
    {
        Guid? sourceId;
        if (request.Id is not null)
        {
            var source = systemRepository.GetRssSourceById(request.Id.Value);
            Guard.NotFound(source);

            source!.Update(request.Name, request.RssUrl);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            sourceId = source.Id;
        }
        else
        {
            var source = systemRepository.GetRssSourceByUrl(request.RssUrl);
            if (source is not null)
                return new CreateOrEditRssResponse(source.Id);

            var folder = systemRepository.GetDefaultFolder();
            Guard.NotFound(folder);

            sourceId = folder!.AddRssSource(request.Name, request.RssUrl);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateOrEditRssResponse(sourceId.Value);
    }
}
