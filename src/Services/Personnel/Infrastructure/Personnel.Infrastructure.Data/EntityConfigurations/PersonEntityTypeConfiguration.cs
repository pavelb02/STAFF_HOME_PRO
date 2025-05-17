using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personnel.Domain.Entities;

namespace Personnel.Infrastructure.Data.EntityConfigurations;

public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");

        builder.OwnsOne(p => p.FullName, fullName =>
        {
            fullName.Property(fn => fn.FirstName).HasColumnName("first_name").HasMaxLength(60).IsRequired();
            fullName.Property(fn => fn.LastName).HasColumnName("last_name").HasMaxLength(60).IsRequired();
            fullName.Property(fn => fn.MiddleName).HasColumnName("middle_name").HasMaxLength(60);
        });

        builder.OwnsOne(p => p.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("email").HasMaxLength(255).IsRequired();
        });
        builder.HasIndex("email").IsUnique();

        builder.OwnsOne(p => p.Phone, phone =>
        {
            phone.Property(pn => pn.Value).HasColumnName("phone").HasMaxLength(12).IsRequired();
        });
        builder.HasIndex("phone").IsUnique();

        builder.Property(p => p.BirthDate).HasColumnName("birth_date").IsRequired();
        builder.Property(p => p.Gender).HasColumnName("gender").IsRequired();
        builder.Property(p => p.AvatarUrl).HasColumnName("avatar_url").HasDefaultValue("https://i.ibb.co/000000/avatar.png");
        builder.Property(p => p.Comment).HasColumnName("comment");

        builder.HasKey(x => x.Id);
        builder
            .HasMany(p => p.WorkExperiences)
            .WithOne()
            .HasForeignKey("person_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}