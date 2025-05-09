using FluentValidation;

namespace Personnel.Domain.Validation;

public class AvatarValidator : AbstractValidator<string>
{
    public AvatarValidator()
    {
        RuleFor(x => x)
            .Must(x => string.IsNullOrEmpty(x) || x.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || x.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Файл аватарки должен быть с расширением .jpg или .png.");
    }
}