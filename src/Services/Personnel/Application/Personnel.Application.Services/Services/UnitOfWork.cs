using Personnel.Application.Services.Interfaces;
using Personnel.Infrastructure.Data;

namespace Personnel.Application.Services.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly StaffHomeProDbContext _dbContext;
    public IPersonRepository Persons { get; }

    public UnitOfWork(StaffHomeProDbContext dbContext, IPersonRepository personRepository)
    {
        _dbContext = dbContext;
        Persons = personRepository;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}