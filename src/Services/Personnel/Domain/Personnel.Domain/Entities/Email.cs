using Ardalis.GuardClauses;

namespace Personnel.Domain.Entities;

public class Email : ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        Guard.Against.NullOrWhiteSpace(value, nameof(Email));

        if (value.Length > 255)
            throw new ArgumentException("Email must not exceed 255 characters.", nameof(Email));

        if (!value.Contains('@'))
            throw new ArgumentException("Email must contain '@' symbol.", nameof(Email));

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLower(); // сравниваем без учёта регистра
    }

    public override string ToString() => Value;
}