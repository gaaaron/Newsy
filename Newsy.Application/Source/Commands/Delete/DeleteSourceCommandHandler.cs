using MediatR;
using Newsy.Domain.Abstractions;

namespace Newsy.Application.Source.Commands.Delete;

internal sealed class DeleteSourceCommandHandler(INewsySystemRepository systemRepository, IUnitOfWork unitOfWork) :
    IRequestHandler<DeleteSourceCommand>
{
    public async Task Handle(DeleteSourceCommand request, CancellationToken cancellationToken)
    {
        systemRepository.DeleteSource(request.Id);
        await unitOfWork.SaveChangesAsync();
    }
}
