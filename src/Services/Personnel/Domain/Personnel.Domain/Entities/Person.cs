using Personnel.Domain.Enum;
using Personnel.Domain.Validation;
using Personnel.Domain.ValueObjects;
using Shared.Domain.Exceptions;

namespace Personnel.Domain.Entities;

/// <summary>
/// Человек
/// </summary>
public class Person
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// ФИО.
    /// </summary>
    public PersonName FullName { get; private set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// GНомер телефона.
    /// </summary>
    public Phone Phone { get; private set; }

    /// <summary>
    /// День рождения.
    /// </summary>
    public DateTime BirthDate { get; private set; }

    /// <summary>
    /// Картинка-аватарка.
    /// </summary>
    public string? AvatarUrl { get; private set; }

    /// <summary>
    /// Гендер.
    /// </summary>
    public Gender Gender { get; private set; }

    /// <summary>
    /// Комментарий.
    /// </summary>
    public string? Comment { get; private set; }

    private readonly List<WorkExperience> _workExperiences = [];

    /// <summary>
    /// Коллекция с информацией об опыте работы.
    /// </summary>
    public IReadOnlyCollection<WorkExperience> WorkExperiences => _workExperiences.AsReadOnly();

    protected Person()
    {
    }

    public Person(string firstName, string lastName, string middleName,
        string email, string phone, DateTime birthDate, Gender gender, string? avatarUrl = null, string? comment = null)
    {
        Id = Guid.NewGuid();
        FullName = new PersonName(firstName, lastName, middleName);
        Email = new Email(email);
        Phone = new Phone(phone);
        BirthDate = birthDate;
        AvatarUrl = SetAvatar(avatarUrl);
        Gender = gender;
        Comment = comment;
    }

    /// <summary>
    /// Присвоить аватарку.
    /// </summary>
    /// <param name="avatarUrl">The new avatar URL of the person. Must be a valid .png or .jpg file path, or null.</param>
    /// <exception cref="ArgumentException">Thrown if the avatar URL does not end with .png or .jpg when not null or empty.</exception>
    private static string? SetAvatar(string? avatarUrl)
    {
        var validator = new AvatarValidator();
        if (avatarUrl == null) return avatarUrl;

        var result = validator.Validate(avatarUrl);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors.First().ErrorMessage);
        }

        return avatarUrl;
    }

    public void Update(string firstName, string lastName, string middleName,
        string email, string phone, DateTime birthDate, Gender gender, string? avatarUrl = null, string? comment = null)
    {
        FullName = new PersonName(firstName, lastName, middleName);
        Email = new Email(email);
        Phone = new Phone(phone);
        BirthDate = birthDate;
        AvatarUrl = SetAvatar(avatarUrl);
        Gender = gender;
        Comment = comment;
    }

    /// <summary>
    /// Добавить Person опыт работы.
    /// </summary>
    public void AddWorkExperience(string position, string organization, string city, string country,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        var workExperience = new WorkExperience(position, organization, city, country, startDate, endDate, description);
        _workExperiences.Add(workExperience);
    }

    /// <summary>
    /// Обновить у Person опыт работы.
    /// </summary>
    public void UpdateWorkExperience(Guid workExperienceId, string position, string organization, string city, string country,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        var workExperience = _workExperiences.FirstOrDefault(x => x.Id == workExperienceId);
        if (workExperience == null)
        {
            throw new EntityNotFoundException("Опыт работы с таким Id отсутствует.");
        }

        workExperience.Update(position, organization, city, country, startDate, endDate, description);
    }

    /// <summary>
    /// Удалить у Person опыт работы.
    /// </summary>
    public void DeleteWorkExperience(Guid workExperienceId)
    {
        var workExperience = _workExperiences.FirstOrDefault(x => x.Id == workExperienceId);
        if (workExperience == null)
        {
            throw new EntityNotFoundException("Опыт работы с таким Id отсутствует.");
        }

        _workExperiences.Remove(workExperience);
    }

    /// <summary>
    /// Получить все опыты работы Person.
    /// </summary>
    public List<WorkExperience> GetAllWorkExperiences()
    {
        var workExperienceList = _workExperiences.ToList();
        return workExperienceList;
    }
}