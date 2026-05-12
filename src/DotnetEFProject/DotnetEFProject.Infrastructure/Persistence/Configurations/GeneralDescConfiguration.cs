using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotnetEFProject.Infrastructure.Persistence.Configurations;

public class GeneralDescConfiguration : IEntityTypeConfiguration<GeneralDesc>
{
    private readonly string? _schemaName;

    public GeneralDescConfiguration(string? schemaName = null)
    {
        _schemaName = schemaName;
    }

    public void Configure(EntityTypeBuilder<GeneralDesc> builder)
    {
        builder.HasKey(e => e.Id).HasName("general_desc_pkey");

        builder.ToTable("general_desc", _schemaName);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.Cond1).HasColumnName("cond1");
        builder.Property(e => e.Cond2).HasColumnName("cond2");
        builder.Property(e => e.Cond3).HasColumnName("cond3");
        builder.Property(e => e.Cond4).HasColumnName("cond4");
        builder.Property(e => e.Cond5).HasColumnName("cond5");
        builder.Property(e => e.CreateDate)
            .HasDefaultValueSql("now()")
            .HasColumnName("create_date");
        builder.Property(e => e.CreateUser).HasColumnName("create_user");
        builder.Property(e => e.Desc1).HasColumnName("desc1");
        builder.Property(e => e.Desc2).HasColumnName("desc2");
        builder.Property(e => e.Desc3).HasColumnName("desc3");
        builder.Property(e => e.Desc4).HasColumnName("desc4");
        builder.Property(e => e.Desc5).HasColumnName("desc5");
        builder.Property(e => e.Gdcode).HasColumnName("gdcode");
        builder.Property(e => e.Gdtype).HasColumnName("gdtype");
        builder.Property(e => e.ModifyDate)
            .HasDefaultValueSql("now()")
            .HasColumnName("modify_date");
        builder.Property(e => e.ModifyUser).HasColumnName("modify_user");
        builder.Property(e => e.Status)
            .HasMaxLength(1)
            .HasDefaultValueSql("'A'::bpchar")
            .HasColumnName("status");

        builder.HasOne(d => d.GdtypeNavigation).WithMany(p => p.GeneralDescs)
            .HasForeignKey(d => d.Gdtype)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("general_desc_gdtype_fkey");
    }
}
