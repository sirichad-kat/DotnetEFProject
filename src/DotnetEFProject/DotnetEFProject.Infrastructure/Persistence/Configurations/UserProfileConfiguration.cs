using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetEFProject.Infrastructure.Persistence.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    private readonly string? _schemaName;

    public UserProfileConfiguration(string? schemaName = null)
    {
        _schemaName = schemaName;
    }

    public void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasKey(e => e.Id).HasName("pk_user_profile");

        builder.ToTable("user_profiles", _schemaName);

        builder.HasIndex(e => e.UserId, "user_profiles_user_id_key").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.Bio).HasColumnName("bio");
        builder.Property(e => e.BirthDate).HasColumnName("birth_date");
        builder.Property(e => e.UserId).HasColumnName("user_id");

        builder.HasOne(d => d.User).WithOne(p => p.UserProfile)
            .HasForeignKey<UserProfile>(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("user_profiles_user_id_fkey");
    }
}
