using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personnel.Domain.Entities;

namespace Personnel.Infrastructure.Data.EntityConfigurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons");

        builder.OwnsOne(p => p.FullName, fullName =>
        {
            fullName.Property(fn => fn.FirstName).HasColumnName("FirstName").IsRequired();
            fullName.Property(fn => fn.LastName).HasColumnName("LastName").IsRequired();
            fullName.Property(fn => fn.MiddleName).HasColumnName("MiddleName");
        });

        builder.OwnsOne(p => p.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("Email").IsRequired();
        });

        builder.OwnsOne(p => p.Phone, phone =>
        {
            phone.Property(pn => pn.Value).HasColumnName("Phone").IsRequired();
        });

        builder.Property(p => p.BirthDate).IsRequired();
        builder.Property(p => p.Gender).IsRequired();
        builder.Property(p => p.AvatarUrl);
        builder.Property(p => p.Comment);

        builder.HasKey(x => x.Id);
        builder
            .HasMany(p => p.WorkExperiences)
            .WithOne()
            .HasForeignKey("PersonId") // теневое свойство
            .OnDelete(DeleteBehavior.Cascade);
    }
}