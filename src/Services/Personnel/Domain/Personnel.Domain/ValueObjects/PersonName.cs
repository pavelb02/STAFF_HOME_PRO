using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using Personnel.Domain.Entities;

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
            ValidateName(value, nameof(FirstName));
            _firstName = value;
        }
    }

    /// <summary>
    /// Свойство имя
    /// </summary>
    public string LastName
    {
        get => _lastName;
        private set
        {
            ValidateName(value, nameof(LastName));
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
            ValidateName(value, nameof(MiddleName));
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
    }

    private void ValidateName(string name, string property)
    {
        Guard.Against.NullOrWhiteSpace(name, property);

        if (name.Length < 2 || name.Length > 60)
            throw new ArgumentException($"{property} must be between 2 and 60 characters.");

        if (!Regex.IsMatch(name, "^[A-Za-zА-Яа-яЁё]+$"))
            throw new ArgumentException($"{property} must contain only letters.");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
        yield return MiddleName;
    }
}