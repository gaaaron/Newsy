using MediatR;
using Newsy.Domain.Abstractions;
using Newsy.Domain.DomainEvents;

namespace Newsy.Application.Content.Events.ContentCreated;
internal class ContentCreatedEventHandler(INewsySystemRepository newsySystemRepository, IUnitOfWork unitOfWork) : 
    INotificationHandler<ContentCreatedEvent>
{
    public async Task Handle(ContentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var nrContents = newsySystemRepository.GetContentsByIds(notification.ContentIds);
        foreach (var content in nrContents)
        {
            var sourceTag = newsySystemRepository.GetSourceTagBySourceId(content.SourceId);
            sourceTag?.Attach(content);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
