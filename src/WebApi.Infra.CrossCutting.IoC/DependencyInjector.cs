using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.AppService.StudentAppService;
using WebApi.Application.AppService.StudentAppService.Interface;
using WebApi.Infra.Data.Context;
using WebApi.Infra.Data.Repositories.StudentRepository;
using WebApi.Services.StudentService;
using WebApi.Services.StudentService.Interface;
using WebApi.Services.AuthService;
using WebApi.Services.AuthService.Interface;
using WebApi.Application.AppService.AuthAppService;
using WebApi.Application.AppService.AuthAppService.Interface;
using Microsoft.EntityFrameworkCore;
using WebApi.Infra.Data.Repositories.UserRepository;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentAppService, StudentAppService>();
        services.AddScoped<IAuthAppService, AuthAppService>();
        return services;
    }

    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WebApiDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
}