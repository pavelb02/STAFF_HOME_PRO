using System.Text.RegularExpressions;
using Ardalis.GuardClauses;

namespace Personnel.Domain.Entities;

public class Phone : ValueObject
{
    private static readonly Regex _pmrPhoneRegex = new(@"^\+373(533|552|210|215|557|219|555|216|562|774|775|777|778|779)\d{5}$");

    public string Value { get; }

    public Phone(string number)
    {
        Guard.Against.NullOrWhiteSpace(number, nameof(Phone));

        if (!_pmrPhoneRegex.IsMatch(number))
            throw new ArgumentException("Invalid PMR phone number format", nameof(number));

        Value = number;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}