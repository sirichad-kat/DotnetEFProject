using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetEFProject.Infrastructure.Persistence;

public abstract class ApplicationDbContext : DbContext
{
    protected string? SchemaName { get; init; }

    protected ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<GeneralDesc> GeneralDescs { get; set; }

    public virtual DbSet<GeneralType> GeneralTypes { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly,
            t => t.Namespace?.StartsWith("DotnetEFProject.Infrastructure.Persistence.Configurations") == true);

        modelBuilder.HasSequence("users_id_seq", SchemaName);

        OnModelCreatingPartial(modelBuilder);
    }

    protected virtual void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
}
