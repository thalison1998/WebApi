namespace WebApi.Infra.Data.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Task CommitAsync();
}