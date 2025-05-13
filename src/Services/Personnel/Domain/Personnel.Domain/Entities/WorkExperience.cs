using Ardalis.GuardClauses;
using FluentValidation;
using Personnel.Domain.Validation;
using Personnel.Domain.ValueObjects;

namespace Personnel.Domain.Entities;

/// <summary>
/// Represents a work experience entity which includes details about the position, organization, address, description, start and end dates.
/// </summary>
public class WorkExperience
{
    /// <summary>
    /// Represents the unique identifier for the work experience entry.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Represents the job title or position.
    /// </summary>
    public string Position { get; private set; }

    /// <summary>
    /// Represents the name of the organization.
    /// </summary>
    public string Organization { get; private set; }

    /// <summary>
    /// Represents the geographical address associated with the work experience.
    /// </summary>
    public Address Address { get; private set; }

    /// <summary>
    /// Provides a description of the work experience.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Represents the starting date of a work experience period.
    /// </summary>
    public DateTime StartDate { get; private set; }

    /// <summary>
    /// Represents the ending date of a work experience period.
    /// </summary>
    public DateTime? EndDate { get; private set; }

    /// <summary>
    /// Отражает сведения об опыте работы, включая должность, организацию, адрес, описание и соответствующие даты.
    /// </summary>
    /// <remarks>
    /// Этот конструктор гарантирует, что данные будут валидны.
    /// </remarks>
    private WorkExperience(string position, string organization, string city, string country,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        Guard.Against.NullOrWhiteSpace(position, nameof(Position));
        Guard.Against.NullOrWhiteSpace(organization, nameof(Organization));

        Id = Guid.NewGuid();
        Position = position;
        Organization = organization;
        Address = SetAddress(city, country);
        StartDate = startDate;
        EndDate = endDate;
        Description = description;

        var validator = new WorkExperienceValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    private static Address SetAddress(string city, string country)
    {
        return new Address(city, country);
    }

    /// <summary>
    /// Добавить опыт работы.
    /// </summary>
    public static WorkExperience AddWorkExperience(string position, string organization, string city, string country,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        var workExperience = new WorkExperience(position, organization, city, country, startDate, endDate, description);

        return workExperience;
    }

    /// <summary>
    /// Обновить опыт работы.
    /// </summary>
    public static WorkExperience UpdateWorkExperience(WorkExperience workExperience, string position, string organization, string city, string country,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        Guard.Against.NullOrWhiteSpace(position, nameof(Position));
        Guard.Against.NullOrWhiteSpace(organization, nameof(Organization));

        workExperience.Position = position;
        workExperience.Organization = organization;
        workExperience.Address = SetAddress(city, country);
        workExperience.StartDate = startDate;
        workExperience.EndDate = endDate;
        workExperience.Description = description;

        var validator = new WorkExperienceValidator();
        var result = validator.Validate(workExperience);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);

        return workExperience;
    }
}