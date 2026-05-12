using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotnetEFProject.Infrastructure.Postgres.Persistence;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<GeneralDesc> GeneralDescs { get; set; }

    public virtual DbSet<GeneralType> GeneralTypes { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=DEMO_DATA_SHADOW;Username=demo;Password=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_course");

            entity.ToTable("courses", "demo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CourseId }).HasName("enrollments_pkey");

            entity.ToTable("enrollments", "demo");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EnrolledAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enrolled_at");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollments_course_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollments_user_id_fkey");
        });

        modelBuilder.Entity<GeneralDesc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("general_desc_pkey");

            entity.ToTable("general_desc", "demo");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cond1).HasColumnName("cond1");
            entity.Property(e => e.Cond2).HasColumnName("cond2");
            entity.Property(e => e.Cond3).HasColumnName("cond3");
            entity.Property(e => e.Cond4).HasColumnName("cond4");
            entity.Property(e => e.Cond5).HasColumnName("cond5");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUser).HasColumnName("create_user");
            entity.Property(e => e.Desc1).HasColumnName("desc1");
            entity.Property(e => e.Desc2).HasColumnName("desc2");
            entity.Property(e => e.Desc3).HasColumnName("desc3");
            entity.Property(e => e.Desc4).HasColumnName("desc4");
            entity.Property(e => e.Desc5).HasColumnName("desc5");
            entity.Property(e => e.Gdcode).HasColumnName("gdcode");
            entity.Property(e => e.Gdtype).HasColumnName("gdtype");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("modify_date");
            entity.Property(e => e.ModifyUser).HasColumnName("modify_user");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("'A'::bpchar")
                .HasColumnName("status");

            entity.HasOne(d => d.GdtypeNavigation).WithMany(p => p.GeneralDescs)
                .HasForeignKey(d => d.Gdtype)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("general_desc_gdtype_fkey");
        });

        modelBuilder.Entity<GeneralType>(entity =>
        {
            entity.HasKey(e => e.Gdtype).HasName("general_type_pkey");

            entity.ToTable("general_type", "demo");

            entity.Property(e => e.Gdtype).HasColumnName("gdtype");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("create_date");
            entity.Property(e => e.CreateUser).HasColumnName("create_user");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("now()")
                .HasColumnName("modify_date");
            entity.Property(e => e.ModifyUser).HasColumnName("modify_user");
            entity.Property(e => e.NameEng).HasColumnName("name_eng");
            entity.Property(e => e.NameLocal).HasColumnName("name_local");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("'A'::bpchar")
                .HasColumnName("status");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_lesson");

            entity.ToTable("lessons", "demo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lessons_course_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_users");

            entity.ToTable("users", "demo");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('users_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(200)
                .HasColumnName("full_name");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_user_profile");

            entity.ToTable("user_profiles", "demo");

            entity.HasIndex(e => e.UserId, "user_profiles_user_id_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.UserProfile)
                .HasForeignKey<UserProfile>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_profiles_user_id_fkey");
        });
        modelBuilder.HasSequence("users_id_seq", "demo");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
