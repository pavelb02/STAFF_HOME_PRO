using Personnel.Application.Services.DTO;

namespace Personnel.Application.Services.Interfaces;

public interface IPersonService
{
    public Guid CreatePerson(CreatePersonRequest createRequest);
    public Guid UpdatePerson(UpdatePersonRequest updateRequest);
    public PersonDto GetPerson(Guid personId);
    public Guid AddWorkExperience(Guid personId, WorkExperienceDto workExperienceDto);
    public void DeleteWorkExperience(Guid personId, Guid workExperienceId);
    public Guid UpdateWorkExperience(Guid personId, UpdateWorkExperienceRequest updateWorkExperienceRequest);
}