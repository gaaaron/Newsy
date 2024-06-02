using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newsy.Application.Common.Behaviours;


namespace Newsy.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddMediatR(cfg => { 
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}
