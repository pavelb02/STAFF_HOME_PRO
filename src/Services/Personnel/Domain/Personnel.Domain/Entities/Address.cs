using Ardalis.GuardClauses;

namespace Personnel.Domain.Entities;

public class Address : ValueObject
{
    public string City { get; }
    public string Country { get; }

    public Address(string city, string country)
    {
        // Проверка, чтобы город и страна не были пустыми или состоящими из пробелов
        Guard.Against.NullOrWhiteSpace(city, nameof(City));
        Guard.Against.NullOrWhiteSpace(country, nameof(Country));

        // Проверка на максимальную длину для города и страны
        if (city.Length > 250)
            throw new ArgumentException("City length must not exceed 250 characters.");
        if (country.Length > 250)
            throw new ArgumentException("Country length must not exceed 250 characters.");

        City = city;
        Country = country;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return City;
        yield return Country;
    }
}