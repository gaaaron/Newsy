using MediatR;

namespace Newsy.Application.Source.Commands.Delete;

public sealed record DeleteSourceCommand(Guid Id) : IRequest;
