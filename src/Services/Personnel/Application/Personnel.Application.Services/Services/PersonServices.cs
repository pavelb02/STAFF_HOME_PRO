using AutoMapper;
using Personnel.Application.Services.DTO;
using Personnel.Application.Services.Interfaces;
using Personnel.Domain.Entities;
using Shared.Domain.Exceptions;

namespace Personnel.Application.Services.Services;

public class PersonServices : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonServices(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public Guid CreatePerson(CreatePersonRequest createRequest)
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

        return _personRepository.Create(person);
    }

    public Guid UpdatePerson(UpdatePersonRequest updateRequest)
    {
        ArgumentNullException.ThrowIfNull(updateRequest);
        var person = _personRepository.GetById(updateRequest.Id);
        if (person == null)
            throw new EntityNotFoundException($"Человек с Id {updateRequest.Id} не найден.");

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

        return _personRepository.Update(person);
    }

    public PersonDto GetPerson(Guid personId)
    {
        var person = _personRepository.GetById(personId);

        return _mapper.Map<PersonDto>(person);
    }

    public Guid AddWorkExperience(Guid personId, WorkExperienceDto workExperienceDto)
    {
        ArgumentNullException.ThrowIfNull(workExperienceDto);
        var person = _personRepository.GetById(personId);
        person.AddWorkExperience(
            workExperienceDto.Position,
            workExperienceDto.Organization,
            workExperienceDto.City,
            workExperienceDto.Country,
            workExperienceDto.StartDate,
            workExperienceDto.EndDate,
            workExperienceDto.Description);
        _personRepository.Update(person);
        return workExperienceDto.Id;
    }

    public void DeleteWorkExperience(Guid personId, Guid workExperienceId)
    {
        var person = _personRepository.GetById(personId);
        person.DeleteWorkExperience(workExperienceId);
        _personRepository.Update(person);
    }

    public Guid UpdateWorkExperience(Guid personId, UpdateWorkExperienceRequest updateWorkExperienceRequest)
    {
        ArgumentNullException.ThrowIfNull(updateWorkExperienceRequest);
        var person = _personRepository.GetById(personId);
        person.UpdateWorkExperience(
            updateWorkExperienceRequest.Id,
            updateWorkExperienceRequest.Position,
            updateWorkExperienceRequest.Organization,
            updateWorkExperienceRequest.City,
            updateWorkExperienceRequest.Country,
            updateWorkExperienceRequest.StartDate,
            updateWorkExperienceRequest.EndDate,
            updateWorkExperienceRequest.Description);
        _personRepository.Update(person);
        return updateWorkExperienceRequest.Id;
    }
}