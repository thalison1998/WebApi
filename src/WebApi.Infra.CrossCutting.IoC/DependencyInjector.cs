using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.AppService.StudentAppService;
using WebApi.Application.AppService.StudentAppService.Interface;
using WebApi.Infra.Data.Context;
using WebApi.Infra.Data.Repositories.StudentRepository;
using WebApi.Infra.Data.Seeders;
using WebApi.Infra.Data.Services;
using WebApi.Infra.Data.UnitOfWork;
using WebApi.Services.StudentService;
using WebApi.Services.StudentService.Interface;

namespace WebApi.Infra.CrossCutting.IoC;

public static class DependencyInjector
{
    public static IServiceCollection InjectApiDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<Seeders>();
        services.AddHostedService<DatabaseSeederHostedService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services
            .InjectDbContext(configuration)
            .InjectAppServices()
            .InjectServices()
            .InjectRepository();
    }

    private static IServiceCollection InjectAppServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentAppService, StudentAppService>();

        return services;
    }
    private static IServiceCollection InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();

        return services;
    }

    private static IServiceCollection InjectRepository(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();

        return services;
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