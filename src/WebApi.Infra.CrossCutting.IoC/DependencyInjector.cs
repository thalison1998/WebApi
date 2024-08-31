using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Infra.Data.Context;
using WebApi.Infra.Data.Seeders;
using WebApi.Infra.Data.Services;

namespace WebApi.Infra.CrossCutting.IoC;

public static class DependencyInjector
{
    public static IServiceCollection InjectApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .InjectDbContext(configuration)
            .AddScoped<Seeders>()
            .AddHostedService<DatabaseSeederHostedService>();
    }

    private static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WebApiDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}