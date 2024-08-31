using WebApi.Infra.Data.Context;

namespace WebApi.Infra.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly WebApiDbContext _context;

    public UnitOfWork(WebApiDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose() => _context?.Dispose();
}

