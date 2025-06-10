namespace Personnel.Application.Services.DTO;

public class WorkExperienceDto
{
    public Guid Id { get; set; }
    public string Position { get; set; } = null!;
    public string Organization { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Description { get; set; }
}