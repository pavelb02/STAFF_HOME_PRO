using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace Personnel.Domain.Entities;

/// <summary>
/// Represents a phone number in the specified PMR phone format.
/// </summary>
public class Phone : ValueObject
{
    private static readonly Regex _pmrPhoneRegex = new(@"^\+373(533|552|210|215|557|219|555|216|562|774|775|777|778|779)\d{5}$");

    private string _value = null!;

    /// <summary>
    /// Gets the validated phone number.
    /// </summary>
    public string Value
    {
        get => _value;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(Phone));

            if (!_pmrPhoneRegex.IsMatch(value))
                throw new ArgumentException("Invalid PMR phone number format", nameof(value));

            _value = value;
        }
    }

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="number">The phone number to validate and store.</param>
    public Phone(string number)
    {
        Value = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <summary>
    /// Возвращает номер телефона в виде строки.
    /// </summary>
    public override string ToString() => Value;
}