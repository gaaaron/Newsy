using MediatR;

namespace Newsy.Application.Tag.Command.UpdateContainsTag;

public sealed record UpdateContainsTagCommand(Guid Id, string Name, string TextToMatch) : IRequest<Guid>;
