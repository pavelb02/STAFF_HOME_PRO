using Person.DataGenerator;
using Personnel.Domain.Enum;

namespace Personnel.Unit;

public class PersonPositiveTests
{
    [Theory]
    [InlineData("Petr", "Pateykin", "Panteleevich", "email@gmail.com", "+37377756981", "01-05-2001", Gender.Male, "", "It's comment, but he is empty.", "Petr")]
    public void ChangePersonPositiveTest(string lastName, string firstName, string middleName, string email,
        string phone, string birthDate, Gender gender, string avatarUrl, string comment, string expectedFirstName)
    {
        //Arrange
        var person = PersonDataGenerator.CreateDefaultPerson();

        //Act
        person.Update(lastName, firstName, middleName, email, phone, Convert.ToDateTime(birthDate), gender, avatarUrl, comment);

        //Assert
        Assert.Equal(expectedFirstName, person.FullName.FirstName);
    }

    [Theory]
    [InlineData("Cleaner", "Sheriff", "Tiraspol", "Moldova", "01-06-2021")]
    public void AddWorkExperiencePositiveTest(string position, string organization, string city, string country, string startDate)
    {
        // Arrange
        var person = PersonDataGenerator.CreateDefaultPerson();

        // Act
        person.AddWorkExperience(position, organization, city, country, Convert.ToDateTime(startDate));

        // Assert
        Assert.Single(person.WorkExperiences);
    }

    [Theory]
    [InlineData("Cleaner", "Sheriff", "Tiraspol", "Moldova", "01-06-2021")]
    public void DeleteWorkExperiencePositiveTest(string position, string organization, string city, string country, string startDate)
    {
        // Arrange
        var person = PersonDataGenerator.CreateDefaultPerson();
        person.AddWorkExperience(position, organization, city, country, Convert.ToDateTime(startDate));
        var workExperienceId = person.WorkExperiences.First().Id;

        // Act
        person.DeleteWorkExperience(workExperienceId);

        // Assert
        Assert.Empty(person.WorkExperiences);
    }
}