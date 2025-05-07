using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace Personnel.Domain.Entities;

public class PersonName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }

    public PersonName(string firstName, string lastName, string middleName)
    {
        ValidateName(firstName, nameof(FirstName));
        ValidateName(lastName, nameof(LastName));
        ValidateName(middleName, nameof(MiddleName));

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