using FluentValidation;

namespace Newsy.Application.Source.Commands.CreateRss;

public sealed class CreateRssNewsSourceCommandValidator : AbstractValidator<CreateRssNewsSourceCommand>
{
    public CreateRssNewsSourceCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
    }
}
