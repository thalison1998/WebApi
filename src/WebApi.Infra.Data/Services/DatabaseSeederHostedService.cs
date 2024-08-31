using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Infra.Data.Context;
using SeederClass = WebApi.Infra.Data.Seeders.Seeders;

namespace WebApi.Infra.Data.Services;

public class DatabaseSeederHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseSeederHostedService> _logger;

    public DatabaseSeederHostedService(IServiceProvider serviceProvider, ILogger<DatabaseSeederHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WebApiDbContext>();
        var seeder = scope.ServiceProvider.GetRequiredService<SeederClass>();

        try
        {
            if (!await dbContext.Database.CanConnectAsync())
            {
                _logger.LogInformation("Database does not exist. Creating database and applying migrations.");
                await dbContext.Database.MigrateAsync();
            }

            await seeder.Seed();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}