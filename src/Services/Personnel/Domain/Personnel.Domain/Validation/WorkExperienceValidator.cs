using FluentValidation;
using Personnel.Domain.Entities;

namespace Personnel.Domain.Validation;

public class WorkExperienceValidator : AbstractValidator<WorkExperience>
{
    public WorkExperienceValidator()
    {
        RuleFor(x => x.Position)
           .MaximumLength(250).WithMessage("Должность не должна превышать 255 символов.");

        RuleFor(x => x.Organization)
          .MaximumLength(250).WithMessage("Организация не должна превышать 255 символов.");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Дата устройства должна быть меньше даты увольнения.");

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("Дата увольнения должна быть меньше, чем дата устройства.");
    }
}