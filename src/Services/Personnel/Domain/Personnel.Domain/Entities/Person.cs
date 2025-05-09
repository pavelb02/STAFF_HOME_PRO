using Personnel.Domain.Enum;
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

    public Person(Guid id, string firstName, string lastName, string middleName,
        Email email, Phone phone, DateTime birthDate, Gender gender, string? avatarUrl = null, string? comment = null)
    {
        Id = id;
        Email = email;
        Phone = phone;
        SetName(new PersonName(firstName, lastName, middleName));
        SetEmail(email);
        SetPhone(phone);
        SetBirthDate(birthDate);
        SetGender(gender);
        SetAvatar(avatarUrl);
        SetComment(comment);
    }

    /// <summary>
    /// Присвоить ФИО.
    /// </summary>
    /// <param name="name">The new name of the person.</param>
    public void SetName(PersonName name) => FullName = name;

    /// <summary>
    /// Присвоить электронную почту.
    /// </summary>
    /// <param name="email">The new email address of the person.</param>
    public void SetEmail(Email email) => Email = email;

    /// <summary>
    /// Присвоить номер телефона.
    /// </summary>
    /// <param name="phone">The new phone number of the person.</param>
    public void SetPhone(Phone phone) => Phone = phone;

    /// <summary>
    /// Присвоить дату рождения.
    /// </summary>
    /// <param name="birthDate">The new birthdate of the person.</param>
    public void SetBirthDate(DateTime birthDate) => BirthDate = birthDate;

    /// <summary>
    /// Присвоить аватарку.
    /// </summary>
    /// <param name="avatarUrl">The new avatar URL of the person. Must be a valid .png or .jpg file path, or null.</param>
    /// <exception cref="ArgumentException">Thrown if the avatar URL does not end with .png or .jpg when not null or empty.</exception>
    public void SetAvatar(string? avatarUrl)
    {
        var validator = new AvatarValidator();
        var result = validator.Validate(avatarUrl);
        if (!result.IsValid)
        {
            throw new ArgumentException(result.Errors.First().ErrorMessage);
        }

        AvatarUrl = avatarUrl;
    }

    /// <summary>
    /// Присвоить гендер.
    /// </summary>
    /// <param name="gender">The new gender of the person.</param>
    public void SetGender(Gender gender) => Gender = gender;

    /// <summary>
    /// Добавить комментарий.
    /// </summary>
    /// <param name="comment">The new comment for the person. Can be null.</param>
    public void SetComment(string? comment) => Comment = comment;
}