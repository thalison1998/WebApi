using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Infra.Data.Context;

namespace WebApi.Infra.CrossCutting.IoC;

public static class DependencyInjector
{
    public static IServiceCollection InjectApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .InjectDbContext(configuration);
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