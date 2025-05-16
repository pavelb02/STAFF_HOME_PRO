using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personnel.Domain.Entities;

namespace Personnel.Infrastructure.Data.EntityConfigurations;

public class WorkExperienceEntityTypeConfiguration : IEntityTypeConfiguration<WorkExperience>
{
    public void Configure(EntityTypeBuilder<WorkExperience> builder)
    {
        builder.ToTable("WorkExperiences");

        builder.OwnsOne(w => w.Address);
        builder.Property(w => w.Position).IsRequired();
        builder.Property(w => w.Organization).IsRequired();
        builder.Property(w => w.Description);
        builder.Property(w => w.StartDate).IsRequired();
        builder.Property(w => w.EndDate);

        builder.HasKey(w => w.Id);
        builder.Property<Guid>("PersonId"); // Теневой FK
    }
}