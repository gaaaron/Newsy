using MediatR;

namespace Newsy.Application.Tag.Command.CreateContainsTag;

public sealed record CreateContainsTagCommand(string Name, string TextToMatch) : IRequest<Guid>;
