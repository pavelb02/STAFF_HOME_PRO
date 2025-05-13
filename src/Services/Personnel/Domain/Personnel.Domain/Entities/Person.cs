using Personnel.Domain.Enum;
using Personnel.Domain.Exceptions;
using Personnel.Domain.Validation;
using Personnel.Domain.ValueObjects;

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

    protected Person(string firstName, string lastName, string middleName,
        string email, string phone, DateTime birthDate, Gender gender, string? avatarUrl = null, string? comment = null)
    {
        Id = Guid.NewGuid();
        FullName = SetName(firstName, lastName, middleName);
        Email = SetEmail(email);
        Phone = SetPhone(phone);
        BirthDate = birthDate;
        AvatarUrl = SetAvatar(avatarUrl);
        Gender = gender;
        Comment = comment;
    }

    /// <summary>
    /// Присвоить ФИО.
    /// </summary>
    /// <param name="name">The new name of the person.</param>
    private static PersonName SetName(string firstName, string lastName, string middleName)
    {
        return new PersonName(firstName, lastName, middleName);
    }

    /// <summary>
    /// Присвоить электронную почту.
    /// </summary>
    /// <param name="email">The new email address of the person.</param>
    private static Email SetEmail(string email)
    {
        return new Email(email);
    }

    /// <summary>
    /// Присвоить номер телефона.
    /// </summary>
    /// <param name="phone">The new phone number of the person.</param>
    private static Phone SetPhone(string phone)
    {
        return new Phone(phone);
    }

    /// <summary>
    /// Присвоить аватарку.
    /// </summary>
    /// <param name="avatarUrl">The new avatar URL of the person. Must be a valid .png or .jpg file path, or null.</param>
    /// <exception cref="ArgumentException">Thrown if the avatar URL does not end with .png or .jpg when not null or empty.</exception>
    private static string? SetAvatar(string? avatarUrl)
    {
        var validator = new AvatarValidator();
        var result = validator.Validate(avatarUrl);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors.First().ErrorMessage);
        }

        return avatarUrl;
    }

    /// <summary>
    /// Добавить Person опыт работы.
    /// </summary>
    public static Guid AddWorkExperienceToPerson(Person person, string position, string organization, string city, string country,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        var workExperience = WorkExperience.AddWorkExperience(position, organization, city, country, startDate, endDate, description);
        person._workExperiences.Add(workExperience);

        return workExperience.Id;
    }

    /// <summary>
    /// Обновить у Person опыт работы.
    /// </summary>
    public static Person UpdateWorkExperience(Person person, Guid workExperienceId, string position, string organization, string city, string country,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        var oldWorkExperience = person._workExperiences.FirstOrDefault(x => x.Id == workExperienceId);
        if (oldWorkExperience == null)
        {
            throw new EntityNotFoundException("Опыт работы с таким Id отсутствует.");
        }

        var newWorkExperience = WorkExperience.UpdateWorkExperience(oldWorkExperience, position, organization, city, country, startDate, endDate, description);
        person._workExperiences.Remove(oldWorkExperience);
        person._workExperiences.Add(newWorkExperience);

        return person;
    }

    /// <summary>
    /// Удалить у Person опыт работы.
    /// </summary>
    public static Person DeleteWorkExperience(Person person, Guid workExperienceId)
    {
        var workExperience = person._workExperiences.FirstOrDefault(x => x.Id == workExperienceId);
        if (workExperience == null)
        {
            throw new EntityNotFoundException("Опыт работы с таким Id отсутствует.");
        }

        person._workExperiences.Remove(workExperience);
        return person;
    }

    /// <summary>
    /// Получить все опыты работы Person.
    /// </summary>
    public static List<WorkExperience> GetAllWorkExperiences(Person person)
    {
        var workExperienceList = person._workExperiences.ToList();
        return workExperienceList;
    }
}