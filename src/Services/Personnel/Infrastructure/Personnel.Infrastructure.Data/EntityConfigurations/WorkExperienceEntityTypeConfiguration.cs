using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personnel.Domain.Entities;

namespace Personnel.Infrastructure.Data.EntityConfigurations;

public class WorkExperienceEntityTypeConfiguration : IEntityTypeConfiguration<WorkExperience>
{
    public void Configure(EntityTypeBuilder<WorkExperience> builder)
    {
        builder.ToTable("work_experiences");

        builder.HasKey(w => w.Id);

        builder.OwnsOne(w => w.Address, address =>
        {
            address.Property(a => a.City)
                .HasColumnName("city")
                .IsRequired()
                .HasMaxLength(250);

            address.Property(a => a.Country)
                .HasColumnName("country")
                .IsRequired()
                .HasMaxLength(250);
        });
        builder.Property(w => w.Position)
            .HasColumnName("position")
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(w => w.Organization)
            .HasColumnName("organization")
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(w => w.Description)
            .HasColumnName("description");

        builder.Property(w => w.StartDate)
            .HasColumnName("start_date")
            .IsRequired();

        builder.Property(w => w.EndDate)
            .HasColumnName("end_date");

        builder.Property<Guid>("person_id");
        builder
            .HasOne<Person>()
            .WithMany()
            .HasForeignKey("person_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}