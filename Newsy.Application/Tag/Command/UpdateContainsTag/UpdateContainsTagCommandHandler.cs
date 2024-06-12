using MediatR;
using Newsy.Application.Common;
using Newsy.Domain.Abstractions;
using Newsy.Domain.Entities;

namespace Newsy.Application.Tag.Command.UpdateContainsTag;

internal sealed class UpdateContainsTagCommandHandler(
    INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<UpdateContainsTagCommand, Guid>
{
    public async Task<Guid> Handle(UpdateContainsTagCommand request, CancellationToken cancellationToken)
    {
        var tag = systemRepository.GetTag(request.Id) as ContainsTag;
        Guard.NotFound(tag, request.Id);

        tag!.Update(request.Name, request.TextToMatch);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return tag.Id;
    }
}
