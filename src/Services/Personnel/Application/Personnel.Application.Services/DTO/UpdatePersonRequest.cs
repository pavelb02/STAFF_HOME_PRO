using Personnel.Domain.Enum;

namespace Personnel.Application.Services.DTO;

public class UpdatePersonRequest
{
    public Guid Id { get; set; }
    public string LastName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Comment { get; set; }
}