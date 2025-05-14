using Personnel.Domain.Entities;
using Personnel.Domain.Enum;

namespace Personnel.Unit;

public class PersonPositiveTests
{
    [Fact]
    public void ChangePersonPositiveTest()
    {
        var person = new Person("Pateykin", "Vasiliy", "Panteleevich", "email@gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
            "It's comment, but he is empty.");
        person.Update("Pateykin", "Petr", "Panteleevich", "email@gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
            "It's comment, but he is empty.");

        Assert.Equal("Petr", person.FullName.LastName);
    }

    [Fact]
    public void AddWorkExperiencePositiveTest()
    {
            var person = new Person("Pateykin", "Vasiliy", "Panteleevich", "email@gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
                "It's comment, but he is empty.");
            person.AddWorkExperience("Cleaner","Sheriff","Tiraspol","Moldova",Convert.ToDateTime("01-06-2021"));

        Assert.Single(person.WorkExperiences);
    }

    [Fact]
    public void DeleteWorkExperiencePositiveTest()
    {
            var person = new Person("Pateykin", "Vasiliy", "Panteleevich", "email@gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
                "It's comment, but he is empty.");
            person.AddWorkExperience("Cleaner","Sheriff","Tiraspol","Moldova",Convert.ToDateTime("01-06-2021"));
            person.DeleteWorkExperience(person.WorkExperiences.First().Id);

        Assert.Empty(person.WorkExperiences);
    }
}