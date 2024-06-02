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
        var tags = newsySystemRepository.GetAllTags();
        foreach (var content in nrContents.Where(x => x.SourceId is not null))
        {
            foreach (var tag in tags) 
            { 
                tag.Attach(content);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
