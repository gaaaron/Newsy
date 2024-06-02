using MediatR;
using Newsy.Domain.Abstractions;

namespace Newsy.Application.Feed.Commands.Delete;

internal sealed class DeleteFeedCommandHandler(INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<DeleteFeedCommand>
{
    public async Task Handle(DeleteFeedCommand request, CancellationToken cancellationToken)
    {
        systemRepository.DeleteFeed(request.Id);
        await unitOfWork.SaveChangesAsync();
    }
}
