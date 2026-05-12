using DotnetEFProject.Infrastructure.Persistence;
using DotnetEFProject.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace DotnetEFProject.Infrastructure.Postgres.Persistence;

public class PostgresApplicationDbContext : ApplicationDbContext
{
    public PostgresApplicationDbContext(
        DbContextOptions<PostgresApplicationDbContext> options,
        IConfiguration? configuration = null)
        : base(options)
    {
        SchemaName = ExtractSchema(configuration);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CourseConfiguration(SchemaName));
        modelBuilder.ApplyConfiguration(new EnrollmentConfiguration(SchemaName));
        modelBuilder.ApplyConfiguration(new GeneralDescConfiguration(SchemaName));
        modelBuilder.ApplyConfiguration(new GeneralTypeConfiguration(SchemaName));
        modelBuilder.ApplyConfiguration(new LessonConfiguration(SchemaName));
        modelBuilder.ApplyConfiguration(new UserConfiguration(SchemaName));
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration(SchemaName));

        modelBuilder.HasSequence("users_id_seq", SchemaName);

        OnModelCreatingPartial(modelBuilder);
    }

    private static string? ExtractSchema(IConfiguration? configuration)
    {
        var connectionString = configuration?.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            return null;

        return TryGetSchemaFromConnectionString(connectionString);
    }

    private static string? TryGetSchemaFromConnectionString(string connectionString)
    {
        try
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            return string.IsNullOrWhiteSpace(builder.SearchPath) ? null : builder.SearchPath;
        }
        catch
        {
            return null;
        }
    }
}
