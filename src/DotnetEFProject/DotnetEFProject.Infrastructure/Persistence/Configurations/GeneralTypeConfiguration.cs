using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetEFProject.Infrastructure.Persistence.Configurations;

public class GeneralTypeConfiguration : IEntityTypeConfiguration<GeneralType>
{
    private readonly string? _schemaName;

    public GeneralTypeConfiguration(string? schemaName = null)
    {
        _schemaName = schemaName;
    }

    public void Configure(EntityTypeBuilder<GeneralType> builder)
    {
        builder.HasKey(e => e.Gdtype).HasName("general_type_pkey");

        builder.ToTable("general_type", _schemaName);

        builder.Property(e => e.Gdtype).HasColumnName("gdtype");
        builder.Property(e => e.CreateDate)
            .HasDefaultValueSql("now()")
            .HasColumnName("create_date");
        builder.Property(e => e.CreateUser).HasColumnName("create_user");
        builder.Property(e => e.ModifyDate)
            .HasDefaultValueSql("now()")
            .HasColumnName("modify_date");
        builder.Property(e => e.ModifyUser).HasColumnName("modify_user");
        builder.Property(e => e.NameEng).HasColumnName("name_eng");
        builder.Property(e => e.NameLocal).HasColumnName("name_local");
        builder.Property(e => e.Status)
            .HasMaxLength(1)
            .HasDefaultValueSql("'A'::bpchar")
            .HasColumnName("status");
    }
}
