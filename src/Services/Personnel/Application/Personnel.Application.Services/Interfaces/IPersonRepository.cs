using Personnel.Domain.Entities;

namespace Personnel.Application.Services.Interfaces;

public interface IPersonRepository
{
    Task<Guid> CreateAsync(Person person);
    Task<Guid> UpdateAsync(Person person);
    Task<Person> GetByIdAsync(Guid personId, bool trackChanges);
    Task<WorkExperience> GetWorkExperienceAsync(Guid personId, Guid workExperienceId);
}