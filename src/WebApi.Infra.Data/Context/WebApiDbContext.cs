using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebApi.Domain.Entitys.Student;
using WebApi.Domain.Entitys.User;

namespace WebApi.Infra.Data.Context;

public class WebApiDbContext : DbContext
{
    public WebApiDbContext(DbContextOptions<WebApiDbContext> options) : base(options) { }

    public DbSet<Student> Student { get; set; }
    public DbSet<User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}