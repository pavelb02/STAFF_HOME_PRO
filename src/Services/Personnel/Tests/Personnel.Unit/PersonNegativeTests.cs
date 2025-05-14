using Personnel.Domain.Entities;
using Personnel.Domain.Enum;
using Personnel.Domain.Exceptions;

namespace Personnel.Unit;

public class PersonNegativeTests
{
    [Fact]
    public void ChangePersonNegativeTest()
    {
        var ex = Assert.Throws<FluentValidation.ValidationException>(() =>
        {
            var person = new Person("Pateykin", "Vasiliy", "Panteleevich", "email@gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
                "It's comment, but he is empty.");
            person.Update("Pateykin", "Petr", "Panteleevich", "email_gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
                "");
        });
        Assert.Contains("Неверный формат Email.", ex.Message);
    }

    [Fact]
    public void AddWorkExperienceNegativeTest()
    {
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            var person = new Person("Pateykin", "Vasiliy", "Panteleevich", "email@gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
                "It's comment, but he is empty.");
            person.AddWorkExperience("","Sheriff","Tiraspol","Moldova",Convert.ToDateTime("01-06-2021"));
        });

        Assert.Contains("Required input Position was empty. (Parameter 'Position')", ex.Message);
    }

    [Fact]
    public void DeleteWorkExperienceNegativeTest()
    {
        var ex = Assert.Throws<EntityNotFoundException>(() =>
        {
            var person = new Person("Pateykin", "Vasiliy", "Panteleevich", "email@gmail.com", "+37377756981", Convert.ToDateTime("01-05-2001"), Gender.Male, "",
                "It's comment, but he is empty.");
            person.AddWorkExperience("Cleaner","Sheriff","Tiraspol","Moldova",Convert.ToDateTime("01-06-2021"));
            person.DeleteWorkExperience(Guid.NewGuid());
        });

        Assert.Contains("Опыт работы с таким Id отсутствует.", ex.Message);
    }
}