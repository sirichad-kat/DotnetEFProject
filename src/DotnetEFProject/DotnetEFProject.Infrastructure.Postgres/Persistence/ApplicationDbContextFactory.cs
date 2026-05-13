using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DotnetEFProject.Infrastructure.Postgres.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DEMO_DATA_SHADOW;Username=democode;Password=postgres");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
