using Personnel.Domain.Enum;
using Personnel.Domain.Validation;
using Personnel.Domain.ValueObjects;

namespace Personnel.Domain.Entities;

/// <summary>
/// Описание сущночти Person
/// </summary>
public class Person
{
    /// <summary>
    /// Gets the unique identifier of the person.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the name of the person.
    /// </summary>
    public PersonName FIO { get; private set; }

    /// <summary>
    /// Gets the email address of the person.
    /// </summary>
    public Email Email { get; private set; }

    /// <summary>
    /// Gets the phone number associated with the person.
    /// </summary>
    public Phone Phone { get; private set; }

    /// <summary>
    /// Gets the birthdate of the person.
    /// </summary>
    public DateTime BirthDate { get; private set; }

    /// <summary>
    /// Gets the avatar URL of the person.
    /// </summary>
    public string? AvatarUrl { get; private set; }

    /// <summary>
    /// Gets the gender of the person.
    /// </summary>
    public Gender Gender { get; private set; }

    /// <summary>
    /// Gets the comment associated with the person. Provides additional details or notes about the person. Can be null.
    /// </summary>
    public string? Comment { get; private set; }

    private readonly List<WorkExperience> _workExperiences = [];

    /// <summary>
    /// Gets the collection of the person's work experiences.
    /// </summary>
    public IReadOnlyCollection<WorkExperience> WorkExperiences => _workExperiences.AsReadOnly();

    public Person(Guid id, string firstName, string lastName, string middleName,
        Email email, Phone phone, DateTime birthDate, Gender gender, PersonName fio, string? avatarUrl = null, string? comment = null)
    {
        Id = id;
        Email = email;
        Phone = phone;
        FIO = fio;
        SetName(new PersonName(firstName, lastName, middleName));
        SetEmail(email);
        SetPhone(phone);
        SetBirthDate(birthDate);
        SetGender(gender);
        SetAvatar(avatarUrl);
        SetComment(comment);
    }

    /// <summary>
    /// Updates the name of the person.
    /// </summary>
    /// <param name="name">The new name of the person.</param>
    public void SetName(PersonName name) => FIO = name;

    /// <summary>
    /// Updates the email of the person.
    /// </summary>
    /// <param name="email">The new email address of the person.</param>
    public void SetEmail(Email email) => Email = email;

    /// <summary>
    /// Updates the phone number of the person.
    /// </summary>
    /// <param name="phone">The new phone number of the person.</param>
    public void SetPhone(Phone phone) => Phone = phone;

    /// <summary>
    /// Updates the birth date of the person.
    /// </summary>
    /// <param name="birthDate">The new birth date of the person.</param>
    public void SetBirthDate(DateTime birthDate) => BirthDate = birthDate;

    /// <summary>
    /// Updates the avatar URL of the person.
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
    /// Updates the gender of the person.
    /// </summary>
    /// <param name="gender">The new gender of the person.</param>
    public void SetGender(Gender gender) => Gender = gender;

    /// <summary>
    /// Updates the comment associated with the person.
    /// </summary>
    /// <param name="comment">The new comment for the person. Can be null.</param>
    public void SetComment(string? comment) => Comment = comment;
}