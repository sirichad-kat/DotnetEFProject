using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetEFProject.Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    private readonly string? _schemaName;

    public CourseConfiguration(string? schemaName = null)
    {
        _schemaName = schemaName;
    }

    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(e => e.Id).HasName("pk_course");

        builder.ToTable("courses", _schemaName);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Price)
            .HasPrecision(10, 2)
            .HasColumnName("price");
        builder.Property(e => e.Title)
            .HasMaxLength(200)
            .HasColumnName("title");
    }
}
