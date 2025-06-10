using Ardalis.GuardClauses;
using FluentValidation;
using Personnel.Domain.Validation;

namespace Personnel.Domain.ValueObjects;

/// <summary>
/// Адрес электронной почты.
/// </summary>
public class Email : ValueObject
{
    private string _value = null!;

    /// <summary>
    /// Получает и записыват адрес электронной почты.
    /// </summary>
    public string Value
    {
        get => _value;
        private set
        {
            Guard.Against.NullOrWhiteSpace(value, nameof(Email));
            _value = value;
        }
    }

    protected Email()
    {
    }

    /// <summary>
    /// Конструктор, вызывающий приватный set.
    /// </summary>
    /// <param name="value"></param>
    public Email(string value)
    {
        Value = value;

        var validator = new EmailValidator();
        var result = validator.Validate(this);
        if(!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLower();
    }

    /// <summary>
    /// Возвращает адрес электронной почты в виде строки.
    /// </summary>
    public override string ToString() => Value;
}