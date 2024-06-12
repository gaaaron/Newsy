using MediatR;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;
using Newsy.Domain.DomainEvents;

namespace Newsy.Application.Source.Events.SourceUpdated;
internal class SourceUpdatedEventHandler(INewsySystemRepository newsySystemRepository, IUnitOfWork unitOfWork) :
    INotificationHandler<SourceUpdatedEvent>
{
    public async Task Handle(SourceUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var tag = newsySystemRepository.GetSourceTagBySourceId(notification.SourceId);
        Guard.NotFound(tag);

        tag!.Update(notification.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
