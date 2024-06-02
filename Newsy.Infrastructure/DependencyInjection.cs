using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newsy.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Newsy.Domain.Abstractions;
using Newsy.Infrastructure.Data.Repositories;
using Newsy.Infrastructure.Data.Interceptors;

namespace Newsy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) => {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            //options.AddInterceptors(sp.GetRequiredService<DispatchDomainEventsInterceptor>()); 
        });

        services.AddTransient<DispatchDomainEventsInterceptor>();
        services.AddTransient<INewsySystemRepository, NewsySystemRepository>();
        services.AddTransient<IUnitOfWork, ApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<IApplicationDbContext, ApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<ApplicationDbContextInitializer>();

        return services;
    }

    public static async Task<IServiceProvider> InitializeInfrastructure(this IServiceProvider services)
    {
        await services.InitializeDatabaseAsync();
        return services;
    }
}
