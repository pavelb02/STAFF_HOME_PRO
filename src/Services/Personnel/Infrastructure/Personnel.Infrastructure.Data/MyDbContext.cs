using Microsoft.EntityFrameworkCore;
using Personnel.Domain.Entities;
using Personnel.Infrastructure.Data.EntityConfigurations;

namespace Personnel.Infrastructure.Data;

public class MyDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }

    public MyDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Port=5432; Database=Staff_Home_Pro; Username=postgres; Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null) throw new ArgumentException(nameof(modelBuilder));

        modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WorkExperienceEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}