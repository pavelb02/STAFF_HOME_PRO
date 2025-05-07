namespace Personnel.Domain.Entities;

public class Person
{
    public Guid Id { get; private set; }
    public PersonName Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string? AvatarUrl { get; private set; }
    public Gender Gender { get; private set; }
    public string? Comment { get; private set; }
    private readonly List<WorkExperience> _workExperiences = new();
    public IReadOnlyCollection<WorkExperience> WorkExperiences => _workExperiences.AsReadOnly();

    public Person(Guid id, PersonName name, Email email, Phone phone, DateTime birthDate, Gender gender)
    {
        Id = id;
        SetName(name);
        SetEmail(email);
        SetPhone(phone);
        SetBirthDate(birthDate);
        SetGender(gender);
    }

    public void SetName(PersonName name) => Name = name;
    public void SetEmail(Email email) => Email = email;
    public void SetPhone(Phone phone) => Phone = phone;
    public void SetBirthDate(DateTime birthDate) => BirthDate = birthDate;

    public void SetAvatar(string? avatarUrl)
    {
        if (!string.IsNullOrEmpty(avatarUrl) &&
            !(avatarUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
              avatarUrl.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase)))
            throw new ArgumentException("Avatar must be a .png or .jpg");

        AvatarUrl = avatarUrl;
    }

    public void SetGender(Gender gender) => Gender = gender;
    public void SetComment(string? comment) => Comment = comment;
}

public enum Gender
{
    Male,
    Female
}