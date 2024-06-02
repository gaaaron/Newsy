using FluentValidation;

namespace Newsy.Application.Source.Commands.CreateOrEditRss;

public sealed class CreateOrEditRssCommandValidator : AbstractValidator<CreateOrEditRssCommand>
{
    public CreateOrEditRssCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
    }
}
