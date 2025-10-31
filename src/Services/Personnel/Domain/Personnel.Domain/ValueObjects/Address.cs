using Ardalis.GuardClauses;
using FluentValidation;
using Personnel.Domain.Validation;

namespace Personnel.Domain.ValueObjects;

/// <summary>
/// Адрес.
/// </summary>
public class Address : ValueObject
{
    private string _city = null!;
    private string _country = null!;

    /// <summary>
    /// Город.
    /// </summary>
    public string City
    {
        get => _city;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(City));
            _city = value;
        }
    }

    /// <summary>
    /// Страна.
    /// </summary>
    public string Country
    {
        get => _country;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(Country));
            _country = value;
        }
    }

    protected Address()
    {
    }

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="city">City name.</param>
    /// <param name="country">Country name.</param>
    protected internal Address(string city, string country)
    {
        City = city;
        Country = country;

        var validator = new AddressValidator();
        var result = validator.Validate(this);
        if(!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Country;
    }
}