using FluentValidation;

namespace Personnel.Domain.Validation;

public class AvatarValidator : AbstractValidator<string>
{
    public AvatarValidator()
    {
        RuleFor(x => x)
            .Must(x => string.IsNullOrEmpty(x) || x.EndsWith(".png", StringComparison.OrdinalIgnoreCase) || x.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Avatar must be a .png or .jpg");
    }
}