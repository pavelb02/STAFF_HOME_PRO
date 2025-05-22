using Personnel.Application.Services.DTO;

namespace Personnel.Application.Services.Interfaces;

public interface IPersonService
{
    public Task<Guid> CreatePersonAsync(CreatePersonRequest createRequest);
    public Task<Guid> UpdatePersonAsync(UpdatePersonRequest updateRequest);
    public Task<PersonDto> GetPersonAsync(Guid personId);
    public Task AddWorkExperienceAsync(Guid personId, WorkExperienceDto workExperienceDto);
    public Task DeleteWorkExperienceAsync(Guid personId, Guid workExperienceId);
    public Task<Guid> UpdateWorkExperienceAsync(Guid personId, UpdateWorkExperienceRequest updateWorkExperienceRequest);
}