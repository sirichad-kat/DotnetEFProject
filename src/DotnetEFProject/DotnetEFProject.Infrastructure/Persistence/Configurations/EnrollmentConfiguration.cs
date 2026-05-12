using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetEFProject.Infrastructure.Persistence.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    private readonly string? _schemaName;

    public EnrollmentConfiguration(string? schemaName = null)
    {
        _schemaName = schemaName;
    }

    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(e => new { e.UserId, e.CourseId }).HasName("enrollments_pkey");

        builder.ToTable("enrollments", _schemaName);

        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.CourseId).HasColumnName("course_id");
        builder.Property(e => e.EnrolledAt)
            .HasDefaultValueSql("now()")
            .HasColumnType("timestamp without time zone")
            .HasColumnName("enrolled_at");

        builder.HasOne(d => d.Course).WithMany(p => p.Enrollments)
            .HasForeignKey(d => d.CourseId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("enrollments_course_id_fkey");

        builder.HasOne(d => d.User).WithMany(p => p.Enrollments)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("enrollments_user_id_fkey");
    }
}
