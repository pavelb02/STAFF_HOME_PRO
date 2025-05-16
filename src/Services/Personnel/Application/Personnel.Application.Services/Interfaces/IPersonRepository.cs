using Personnel.Domain.Entities;

namespace Personnel.Application.Services.Interfaces;

public interface IPersonRepository
{
    Guid Create(Person person);
    Guid Update(Person person);
    Person GetById(Guid personId);
    Guid AddWorkExperience(Guid personId, WorkExperience workExperience);
    WorkExperience GetWorkExperience(Guid personId, Guid workExperienceId);
    Guid UpdateWorkExperience(Guid personId, Guid workExperienceId, WorkExperience workExperience);
}