using FluentValidation;
using Personnel.Domain.ValueObjects;

namespace Personnel.Domain.Validation;

public class PersonNameValidator : AbstractValidator<PersonName>
{
    public PersonNameValidator()
    {
        RuleFor(x => x.FirstName)
            .Length(2, 60).WithMessage($"Фамилия должна быть в диапазоне от 2 до 60 символов")
            .Matches("^[A-Za-zА-Яа-яЁё]+$").WithMessage($"Фамилия должна состоять только из букв.");
        
        RuleFor(x => x.LastName)
            .Length(2, 60).WithMessage($"Имя должно быть в диапазоне от 2 до 60 символов")
            .Matches("^[A-Za-zА-Яа-яЁё]+$").WithMessage($"Имя должно состоять только из букв.");
        
        RuleFor(x => x.MiddleName)
            .Length(2, 60).WithMessage($"Отчество должно быть в диапазоне от 2 до 60 символов")
            .Matches("^[A-Za-zА-Яа-яЁё]+$").WithMessage($"Отчество должно состоять только из букв.");
    }
}