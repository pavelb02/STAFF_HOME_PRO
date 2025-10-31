namespace Personnel.Application.Services.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPersonRepository Persons { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}