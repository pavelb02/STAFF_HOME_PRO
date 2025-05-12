using Ardalis.GuardClauses;
using FluentValidation;
using Personnel.Domain.Validation;

namespace Personnel.Domain.ValueObjects;

/// <summary>
/// Класс, описывающий ФИО человека.
/// </summary>
public class PersonName : ValueObject
{
    private string _firstName = null!;
    private string _lastName = null!;
    private string _middleName = null!;

    /// <summary>
    /// Свойство фамилия.
    /// </summary>
    public string FirstName
    {
        get => _firstName;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(FirstName));
            _firstName = value;
        }
    }

    /// <summary>
    /// Свойство имя.
    /// </summary>
    public string LastName
    {
        get => _lastName;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(LastName));
            _lastName = value;
        }
    }

    /// <summary>
    /// Свойство отчество.
    /// </summary>
    public string MiddleName
    {
        get => _middleName;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(MiddleName));
            _middleName = value;
        }
    }

    /// <summary>
    /// Конструктор класса
    /// </summary>
    public PersonName(string firstName, string lastName, string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;

        var validator = new PersonNameValidator();
        var result = validator.Validate(this);
        if(!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}