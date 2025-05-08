using Ardalis.GuardClauses;
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
    public WorkExperience(Guid id, string position, string organization, Address address,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        Guard.Against.NullOrWhiteSpace(position, nameof(Position));
        Guard.Against.NullOrWhiteSpace(organization, nameof(Organization));

        if (position.Length > 250)
            throw new ArgumentException("Position max length is 250");

        if (organization.Length > 250)
            throw new ArgumentException("Organization max length is 250");

        if (endDate.HasValue && startDate > endDate.Value)
            throw new ArgumentException("StartDate must be before EndDate");

        Id = id;
        Position = position;
        Organization = organization;
        Address = address;
        StartDate = startDate;
        EndDate = endDate;
        Description = description;
    }
}