using Microsoft.EntityFrameworkCore;
using Personnel.Application.Services.Interfaces;
using Personnel.Domain.Entities;
using Personnel.Infrastructure.Data;

namespace Personnel.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly MyDbContext _dbContext;

    public PersonRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid Create(Person person)
    {
        _dbContext.Persons.Add(person);
        _dbContext.SaveChanges();
        return person.Id;
    }

    public Guid Update(Person person)
    {
        _dbContext.Entry(person).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return person.Id;
    }

    public Person GetById(Guid personId)
    {
        var person = _dbContext.Persons.FirstOrDefault(c=>c.Id == personId);
        if (person == null)
        {
            throw new KeyNotFoundException($"Клиент с Id {personId} не найден.");
        }

        return person;
    }

    public Guid AddWorkExperience(Guid personId, WorkExperience workExperience)
    {
        return workExperience.Id;
    }

    public WorkExperience GetWorkExperience(Guid personId, Guid workExperienceId)
    {
        var person = _dbContext.Persons.Include(person => person.WorkExperiences).FirstOrDefault(c=>c.Id == personId);
        if (person == null)
        {
            throw new KeyNotFoundException($"Клиент с Id {personId} не найден.");
        }

        var workExperience = person.WorkExperiences.FirstOrDefault(c=>c.Id == workExperienceId);
        if (workExperience == null)
        {
            throw new KeyNotFoundException($"Опыт работы с Id {personId} не найден.");
        }

        return workExperience;
    }

    public Guid UpdateWorkExperience(Guid personId, Guid workExperienceId, WorkExperience workExperience)
    {
        return workExperienceId;
    }
}