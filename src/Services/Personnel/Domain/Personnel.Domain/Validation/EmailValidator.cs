using FluentValidation;
using Personnel.Domain.ValueObjects;

namespace Personnel.Domain.Validation;

public class EmailValidator : AbstractValidator<Email>
{
    public EmailValidator()
    {
        RuleFor(x => x.Value)
            .MaximumLength(255).WithMessage("Email не может содержать более 255 символов.")
            .EmailAddress().WithMessage("Неверный формат Email.");
    }
}