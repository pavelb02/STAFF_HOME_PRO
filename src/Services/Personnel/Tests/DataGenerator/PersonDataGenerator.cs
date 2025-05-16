using Personnel.Domain.Entities;
using Personnel.Domain.Enum;

namespace DataGenerator;

public static class PersonDataGenerator
{
    public static Person CreateDefaultPerson()
    {
        return new Person(
            "Pateykin",
            "Vasiliy",
            "Panteleevich",
            "email@gmail.com",
            "+37377756981",
            Convert.ToDateTime("01-05-2001"),
            Gender.Male,
            "",
            "It's comment, but he is empty."
        );
    }
}