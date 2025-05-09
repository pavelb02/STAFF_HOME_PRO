using FluentValidation;
using Personnel.Domain.ValueObjects;

namespace Personnel.Domain.Validation;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Country)
            .MaximumLength(250)
            .WithMessage("Длина страны не должна превышать 255 символов.");

        RuleFor(x => x.City)
            .MaximumLength(250)
            .WithMessage("Длина города не должна превышать 255 символов.");
    }
}