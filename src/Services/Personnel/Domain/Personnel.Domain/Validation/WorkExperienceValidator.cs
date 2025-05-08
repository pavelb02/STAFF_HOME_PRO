using FluentValidation;
using Personnel.Domain.Entities;

namespace Personnel.Domain.Validation;

public class WorkExperienceValidator : AbstractValidator<WorkExperience>
{
    public WorkExperienceValidator()
    {
        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position is required")
            .MaximumLength(250).WithMessage("Position max length is 250");

        RuleFor(x => x.Organization)
            .NotEmpty().WithMessage("Organization is required")
            .MaximumLength(250).WithMessage("Organization max length is 250");

        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("StartDate must be before EndDate");

        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
            .WithMessage("EndDate must be after StartDate");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required");
    }
}