using Personnel.Domain.Enum;

namespace Person.DataGenerator;

public static class PersonDataGenerator
{
    public static Personnel.Domain.Entities.Person CreateDefaultPerson()
    {
        return new Personnel.Domain.Entities.Person(
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