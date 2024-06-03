using MediatR;
using Newsy.Domain.Abstractions;
using Newsy.Domain.DomainEvents;
using Newsy.Domain.Entities;

namespace Newsy.Application.Source.Events.SourceCreated;
internal class SourceCreatedEventHandler(INewsySystemRepository newsySystemRepository, IUnitOfWork unitOfWork) :
    INotificationHandler<SourceCreatedEvent>
{
    public async Task Handle(SourceCreatedEvent notification, CancellationToken cancellationToken)
    {
        var tag = SourceTag.Create(notification.Name, notification.SourceId);
        newsySystemRepository.InsertTag(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
