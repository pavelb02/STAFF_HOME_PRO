using FluentValidation;
using Personnel.Domain.Entities;

namespace Personnel.Domain.Validation;

public class PhoneValidator : AbstractValidator<Phone>
{
    public PhoneValidator()
    {
        RuleFor(x => x.Value)
            .Matches(@"^\+373(533|552|210|215|557|219|555|216|562|774|775|777|778|779)\d{5}$")
            .WithMessage("Телнфон не соответсвует формату ПМР.");
    }
}