using DataGenerator;
using Personnel.Domain.Enum;
using Personnel.Domain.Exceptions;

namespace Personnel.Unit;

public class PersonNegativeTests
{
    [Theory]
    [InlineData("Pateykin", "Petr", "Panteleevich", "email_gmail.com", "+37377756981", "01-05-2001", Gender.Male, "", "", "Неверный формат Email.")]
    public void ChangePersonNegativeTest(string lastName, string firstName, string middleName, string email,
        string phone, string birthDate, Gender gender, string avatarUrl, string comment, string expectedErrorMessage)
    {
        // Arrange
        var person = PersonDataGenerator.CreateDefaultPerson();

        // Act & Assert
       Assert.Throws<FluentValidation.ValidationException>(() =>
            person.Update(lastName, firstName, middleName, email, phone, Convert.ToDateTime(birthDate), gender, avatarUrl, comment)
        );
    }

    [Theory]
    [InlineData("", "Sheriff", "Tiraspol", "Moldova", "01-06-2021", "Position")]
    public void AddWorkExperienceNegativeTest(string position, string organization, string city, string country, string startDate, string expectedParamName)
    {
        // Arrange
        var person = PersonDataGenerator.CreateDefaultPerson();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            person.AddWorkExperience(position, organization, city, country, Convert.ToDateTime(startDate))
        );
    }

    [Theory]
    [InlineData("Cleaner", "Sheriff", "Tiraspol", "Moldova", "01-06-2021")]
    public void DeleteWorkExperienceNegativeTest(string position, string organization, string city, string country, string startDate)
    {
        // Arrange
        var person = PersonDataGenerator.CreateDefaultPerson();
        person.AddWorkExperience(position, organization, city, country, Convert.ToDateTime(startDate));

        // Act & Assert
        Assert.Throws<EntityNotFoundException>(() => { person.DeleteWorkExperience(Guid.NewGuid()); });
    }
}