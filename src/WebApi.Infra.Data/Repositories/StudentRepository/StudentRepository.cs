using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entitys.Student;
using WebApi.Infra.Data.Context;

namespace WebApi.Infra.Data.Repositories.StudentRepository;

public class StudentRepository : IStudentRepository
{
    private readonly WebApiDbContext _context;

    public StudentRepository(WebApiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _context.Student.ToListAsync();
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        return await _context.Student.FindAsync(id);
    }

    public async Task AddStudentAsync(Student student)
    {
        await _context.Student.AddAsync(student);
    }

    public async Task UpdateStudentAsync(Student student)
    {
        _context.Update(student);
        await Task.CompletedTask;
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _context.Student.FindAsync(id);
        if (student != null)
        {
            _context.Student.Remove(student);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
