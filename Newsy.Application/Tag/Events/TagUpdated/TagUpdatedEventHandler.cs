using MediatR;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;
using Newsy.Domain.DomainEvents;

namespace Newsy.Application.Tag.Events.TagUpdated;

internal class TagUpdatedEventHandler(INewsySystemRepository newsySystemRepository, IUnitOfWork unitOfWork) :
    INotificationHandler<TagUpdatedEvent>
{
    public async Task Handle(TagUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var tag = newsySystemRepository.GetTag(notification.TagId);
        Guard.NotFound(tag, notification.TagId);

        var contents = newsySystemRepository.GetAllContents();

        foreach (var content in contents)
        {
            tag!.Attach(content);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
