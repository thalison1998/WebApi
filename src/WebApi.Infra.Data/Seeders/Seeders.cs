using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using WebApi.Domain.Entitys.Student;
using WebApi.Domain.Entitys.User;
using WebApi.Infra.Data.Context;
using WebApi.Infra.Data.Seeders.Mappings;

namespace WebApi.Infra.Data.Seeders;

public class Seeders
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Seeders> _logger;

    public Seeders(IServiceProvider serviceProvider, ILogger<Seeders> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    private async Task SeedUser(WebApiDbContext dbContext)
    {
        var user = User.Create("admin", "ubc2024");

        dbContext.User.Add(user);

        _logger.LogInformation("User '{Username}' added to the database.", user.UserName);
    }

    public async Task Seed()
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetService<WebApiDbContext>() ?? throw new NullReferenceException();

        if (await dbContext.Student.AnyAsync())
        {
            _logger.LogInformation("Database already seeded.");
            return;
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            await SeedFromCsv(dbContext);
            await SeedUser(dbContext);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            _logger.LogInformation("Database seeded successfully.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedFromCsv(WebApiDbContext dbContext)
    {
        var baseDirectory = AppContext.BaseDirectory;
        var projectRoot = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\..\"));
        var csvFilePath = Path.Combine(projectRoot, "WebApi.Infra.Data", "Seeders", "StudentSeeder", "StudentSeeder.csv");

        using var reader = new StreamReader(csvFilePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        });

        csv.Context.RegisterClassMap<StudentMap>();

        var studentDtos = csv.GetRecords<StudentDto>().ToList();
        var students = studentDtos.Select(dto => Student.Create(dto.Name, dto.Age, dto.Grade, dto.AverageGrade, dto.Address, dto.FatherName, dto.MotherName, DateTime.SpecifyKind(dto.BirthDate, DateTimeKind.Utc))).ToList();

        dbContext.Student.AddRange(students);
        _logger.LogInformation("{Count} students added to the database.", students.Count);
    }
}