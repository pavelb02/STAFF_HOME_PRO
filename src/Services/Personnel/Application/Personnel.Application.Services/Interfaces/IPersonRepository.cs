using Personnel.Domain.Entities;

namespace Personnel.Application.Services.Interfaces;

public interface IPersonRepository
{
    Guid Create(Person person);
    Guid Update(Person person);
    Person GetById(Guid personId);
    WorkExperience GetWorkExperience(Guid personId, Guid workExperienceId);
}