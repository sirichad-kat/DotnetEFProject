using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetEFProject.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly string? _schemaName;

    public UserConfiguration(string? schemaName = null)
    {
        _schemaName = schemaName;
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id).HasName("pk_users");

        builder.ToTable("users", _schemaName);

        builder.HasIndex(e => e.Email, "users_email_key").IsUnique();

        builder.Property(e => e.Id)
            .HasDefaultValueSql("nextval('users_id_seq'::regclass)")
            .HasColumnName("id");
        builder.Property(e => e.Email)
            .HasMaxLength(200)
            .HasColumnName("email");
        builder.Property(e => e.FullName)
            .HasMaxLength(200)
            .HasColumnName("full_name");
    }
}
