using MediatR;
using Newsy.Domain.Abstractions;

namespace Newsy.Application.Feed.Commands.Create;
internal sealed class CreateFeedCommandHandler(
    INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateFeedCommand, CreateFeedResponse>
{
    public async Task<CreateFeedResponse> Handle(CreateFeedCommand request, CancellationToken cancellationToken)
    {
        var feed = systemRepository.GetFeedByName(request.Name);
        if (feed is not null)
            return new CreateFeedResponse(feed.Id);

        feed = new Domain.Entities.Feed(Guid.NewGuid(), request.Name);
        systemRepository.InsertFeed(feed);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateFeedResponse(feed.Id);
    }
}
