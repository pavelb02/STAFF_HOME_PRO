using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Personnel.Infrastructure.Data
{
    public class StaffHomeProDbContextFactory : IDesignTimeDbContextFactory<StaffHomeProDbContext>
    {
        public StaffHomeProDbContext CreateDbContext(string[] args)
        {
            try
            {
                var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Api", "Personnel.Api"));
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<StaffHomeProDbContext>();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseNpgsql(connectionString,
                    x => x.MigrationsAssembly(typeof(StaffHomeProDbContext).Assembly.FullName));

                return new StaffHomeProDbContext(optionsBuilder.Options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreateDbContext: {ex.Message}");
                throw;
            }
        }
    }
}