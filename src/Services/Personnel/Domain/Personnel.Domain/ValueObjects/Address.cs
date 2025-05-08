using Ardalis.GuardClauses;
using Personnel.Domain.Entities;

namespace Personnel.Domain.ValueObjects;

/// <summary>
/// Represents an address as a value object, consisting of a city and a country.
/// </summary>
public class Address : ValueObject
{
    private string _city = null!;
    private string _country = null!;

    /// <summary>
    /// Gets the name of the city associated with the address.
    /// </summary>
    public string City
    {
        get => _city;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(City));
            if (value.Length > 250)
                throw new ArgumentException("City length must not exceed 250 characters.", nameof(City));

            _city = value;
        }
    }

    /// <summary>
    /// Gets the name of the country associated with the address.
    /// </summary>
    public string Country
    {
        get => _country;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(Country));
            if (value.Length > 250)
                throw new ArgumentException("Country length must not exceed 250 characters.", nameof(Country));

            _country = value;
        }
    }

    /// <summary>
    /// Конструктор класса
    /// </summary>
    /// <param name="city">City name.</param>
    /// <param name="country">Country name.</param>
    public Address(string city, string country)
    {
        City = city;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Country;
    }
}