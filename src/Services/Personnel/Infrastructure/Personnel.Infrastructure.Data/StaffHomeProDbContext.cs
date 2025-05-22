using Microsoft.EntityFrameworkCore;
using Personnel.Domain.Entities;
using Personnel.Infrastructure.Data.EntityConfigurations;

namespace Personnel.Infrastructure.Data;

public class StaffHomeProDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<WorkExperience> WorkExperiences { get; set; }

    public StaffHomeProDbContext(DbContextOptions<StaffHomeProDbContext> options)
        : base(options)
    {
    }

    /* для Program.cs
     
     var builder = WebApplication.CreateBuilder(args);
     // builder.Configuration уже загружает appsettings.json

    builder.Services.AddDbContext<StaffHomeProDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StaffHomeProDatabase")));
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null) throw new ArgumentException(nameof(modelBuilder));

        modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WorkExperienceEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}