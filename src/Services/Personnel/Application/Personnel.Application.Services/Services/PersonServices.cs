using AutoMapper;
using Personnel.Application.Services.DTO;
using Personnel.Application.Services.Interfaces;
using Personnel.Domain.Entities;

namespace Personnel.Application.Services.Services;

public class PersonServices : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PersonServices(IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreatePersonAsync(CreatePersonRequest createRequest)
    {
        ArgumentNullException.ThrowIfNull(createRequest);
        var person = new Person(
            createRequest.FirstName,
            createRequest.LastName,
            createRequest.MiddleName,
            createRequest.Email,
            createRequest.Phone,
            createRequest.BirthDate,
            createRequest.Gender,
            createRequest.AvatarUrl,
            createRequest.Comment
        );

        await _personRepository.CreateAsync(person);
        await _unitOfWork.SaveChangesAsync();
        return person.Id;
    }

    public async Task<Guid> UpdatePersonAsync(UpdatePersonRequest updateRequest)
    {
        ArgumentNullException.ThrowIfNull(updateRequest);
        var person = await _personRepository.GetByIdAsync(updateRequest.Id, true).ConfigureAwait(false);

        person.Update(
            updateRequest.FirstName,
            updateRequest.LastName,
            updateRequest.MiddleName,
            updateRequest.Email,
            updateRequest.Phone,
            updateRequest.BirthDate,
            updateRequest.Gender,
            updateRequest.AvatarUrl,
            updateRequest.Comment
        );

        await _unitOfWork.SaveChangesAsync();
        return person.Id;
    }

    public async Task<PersonDto> GetPersonAsync(Guid personId)
    {
        var person = await _personRepository.GetByIdAsync(personId, false).ConfigureAwait(false);

        return _mapper.Map<PersonDto>(person);
    }

    public async Task AddWorkExperienceAsync(Guid personId, WorkExperienceDto workExperienceDto)
    {
        ArgumentNullException.ThrowIfNull(workExperienceDto);
        var person = await _personRepository.GetByIdAsync(personId, true).ConfigureAwait(false);
        person.AddWorkExperience(
            workExperienceDto.Position,
            workExperienceDto.Organization,
            workExperienceDto.City,
            workExperienceDto.Country,
            workExperienceDto.StartDate,
            workExperienceDto.EndDate,
            workExperienceDto.Description);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteWorkExperienceAsync(Guid personId, Guid workExperienceId)
    {
        var person = await _personRepository.GetByIdAsync(personId, true).ConfigureAwait(false);
        person.DeleteWorkExperience(workExperienceId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Guid> UpdateWorkExperienceAsync(Guid personId, UpdateWorkExperienceRequest updateWorkExperienceRequest)
    {
        ArgumentNullException.ThrowIfNull(updateWorkExperienceRequest);
        var person = await _personRepository.GetByIdAsync(personId, true).ConfigureAwait(false);
        person.UpdateWorkExperience(
            updateWorkExperienceRequest.Id,
            updateWorkExperienceRequest.Position,
            updateWorkExperienceRequest.Organization,
            updateWorkExperienceRequest.City,
            updateWorkExperienceRequest.Country,
            updateWorkExperienceRequest.StartDate,
            updateWorkExperienceRequest.EndDate,
            updateWorkExperienceRequest.Description);

        await _unitOfWork.SaveChangesAsync();
        return updateWorkExperienceRequest.Id;
    }
}