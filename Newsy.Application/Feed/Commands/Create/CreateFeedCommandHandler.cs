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

        feed = Domain.Entities.Feed.Create(request.Name);
        systemRepository.InsertFeed(feed);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new CreateFeedResponse(feed.Id);
    }
}
