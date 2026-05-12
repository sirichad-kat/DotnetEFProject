using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetEFProject.Infrastructure.Persistence.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    private readonly string? _schemaName;

    public LessonConfiguration(string? schemaName = null)
    {
        _schemaName = schemaName;
    }

    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasKey(e => e.Id).HasName("pk_lesson");

        builder.ToTable("lessons", _schemaName);

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.CourseId).HasColumnName("course_id");
        builder.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
        builder.Property(e => e.Title)
            .HasMaxLength(200)
            .HasColumnName("title");

        builder.HasOne(d => d.Course).WithMany(p => p.Lessons)
            .HasForeignKey(d => d.CourseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("lessons_course_id_fkey");
    }
}
