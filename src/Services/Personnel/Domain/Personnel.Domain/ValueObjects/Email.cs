using Ardalis.GuardClauses;

namespace Personnel.Domain.Entities;

/// <summary>
/// Email address.
/// </summary>
public class Email : ValueObject
{
    private string _value = null!;

    /// <summary>
    /// Gets the email address value.
    /// </summary>
    /// <remarks>
    /// The value is a string representing a valid email address. It must not be null,
    /// empty, or consist only of whitespace. The maximum length allowed is 255 characters,
    /// and the value must contain the '@' symbol to be considered a valid email.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown when the value exceeds 255 characters or does not contain the '@' symbol.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown when the value is null or consists of only whitespace.
    /// </exception>
    public string Value
    {
        get => _value;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(Email));

            if (value.Length > 255)
                throw new ArgumentException("Email must not exceed 255 characters.", nameof(Email));

            if (!value.Contains('@'))
                throw new ArgumentException("Email must contain '@' symbol.", nameof(Email));

            _value = value;
        }
    }

    /// <summary>
    /// Конструктор, вызывающий приватный set.
    /// </summary>
    /// <param name="value"></param>
    public Email(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLower();
    }

    /// <summary>
    /// Returns a string representation of the email value.
    /// </summary>
    /// <returns>The email address as a string.</returns>
    public override string ToString() => Value;
}