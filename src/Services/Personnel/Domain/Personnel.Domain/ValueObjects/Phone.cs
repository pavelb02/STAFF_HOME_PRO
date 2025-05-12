using Ardalis.GuardClauses;
using FluentValidation;
using Personnel.Domain.Validation;

namespace Personnel.Domain.ValueObjects;

/// <summary>
/// Номер телефона.
/// </summary>
public class Phone : ValueObject
{
    private string _value = null!;

    /// <summary>
    /// Получает и записыват номер телефона.
    /// </summary>
    public string Value
    {
        get => _value;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(Phone));

            _value = value;
        }
    }

    /// <summary>
    /// Конструктор класса
    /// </summary>
    public Phone(string number)
    {
        Value = number;
        
        var validator = new PhoneValidator();
        var result = validator.Validate(this);
        if(!result.IsValid)
            throw new ValidationException(result.Errors.ToString());
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