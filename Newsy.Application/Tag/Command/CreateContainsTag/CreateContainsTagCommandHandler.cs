using MediatR;
using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;

namespace Newsy.Application.Tag.Command.CreateContainsTag;

internal sealed class CreateContainsTagCommandHandler(
    INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<CreateContainsTagCommand, Guid>
{
    public async Task<Guid> Handle(CreateContainsTagCommand request, CancellationToken cancellationToken)
    {
        var tag = ContainsTag.Create(request.Name, request.TextToMatch);
        systemRepository.InsertTag(tag);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return tag.Id;
    }
}
