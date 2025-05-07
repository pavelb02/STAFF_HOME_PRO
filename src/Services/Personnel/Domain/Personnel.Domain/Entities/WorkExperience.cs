using Ardalis.GuardClauses;

namespace Personnel.Domain.Entities;

public class WorkExperience
{
    public Guid Id { get; private set; }
    public string Position { get; private set; }
    public string Organization { get; private set; }
    public Address Address { get; private set; }
    public string? Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }

    public WorkExperience(Guid id, string position, string organization, Address address,
        DateTime startDate, DateTime? endDate = null, string? description = null)
    {
        Guard.Against.NullOrWhiteSpace(position);
        Guard.Against.NullOrWhiteSpace(organization);

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