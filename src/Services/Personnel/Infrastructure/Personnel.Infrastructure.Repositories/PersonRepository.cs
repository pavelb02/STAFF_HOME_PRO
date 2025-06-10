using Microsoft.EntityFrameworkCore;
using Personnel.Application.Services.Interfaces;
using Personnel.Domain.Entities;
using Personnel.Infrastructure.Data;
using Shared.Domain.Exceptions;

namespace Personnel.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly StaffHomeProDbContext _dbContext;

    public PersonRepository(StaffHomeProDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Guid> CreateAsync(Person person)
    {
        _dbContext.Persons.Add(person);
        return Task.FromResult(person.Id);
    }

    public Task<Guid> UpdateAsync(Person person)
    {
        return Task.FromResult(person.Id);
    }

    public async Task<Person> GetByIdAsync(Guid personId, bool trackChanges = true)
    {
        var query = _dbContext.Persons.Include(p=>p.WorkExperiences).AsQueryable();

        if (!trackChanges)
            query = query.AsNoTracking();

        var person = await query.FirstOrDefaultAsync(c => c.Id == personId).ConfigureAwait(false);

        if (person == null)
        {
            throw new EntityNotFoundException($"Клиент с Id {personId} не найден.");
        }

        return person;
    }

    public async Task<WorkExperience> GetWorkExperienceAsync(Guid personId, Guid workExperienceId)
    {
        var workExperience = await _dbContext.Persons
            .Where(p => p.Id == personId)
            .SelectMany(p => p.WorkExperiences)
            .Where(w => w.Id == workExperienceId)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

        if (workExperience == null)
            throw new EntityNotFoundException($"Опыт работы с Id {workExperienceId} не найден у клиента {personId}.");

        return workExperience;
    }
}