using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using WebApi.Domain.Entitys.Student;
using WebApi.Infra.Data.Context;
using WebApi.Infra.Data.Seeders.Mappings;

namespace WebApi.Infra.Data.Seeders
{
    public class Seeders
    {
        private readonly IServiceProvider _serviceProvider;

        public Seeders(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Seed()
        {
            using IServiceScope scope = _serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetService<WebApiDbContext>() ?? throw new NullReferenceException();

            if (await dbContext.Student.AnyAsync())
            {
                return;
            }

            await using var tran = await dbContext.Database.BeginTransactionAsync();

            try
            {
                await SeedFromCsv(dbContext);
                await tran.CommitAsync();
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                await tran.RollbackAsync();
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

            var students = studentDtos.Select(dto => Student.Create(dto.Name, dto.Age, dto.Grade, dto.AverageGrade, dto.Address, dto.FatherName, dto.MotherName, dto.BirthDate)).ToList();

            dbContext.Student.AddRange(students);
        }

    }
}