using DotnetEFProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetEFProject.Infrastructure.Postgres.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1 : 1 relationship between User and UserProfile
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("full_name");

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasIndex(x => x.Email)
                    .IsUnique();
            });
            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.ToTable("user_profiles");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Bio)
                    .HasColumnType("text");

                entity.Property(x => x.BirthDate)
                    .HasColumnType("date");

                entity.HasOne(x => x.User)
                    .WithOne(x => x.Profile)
                    .HasForeignKey<UserProfile>(x => x.UserId);
            });

            // 1 : N relationship between Course and Lesson
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("courses");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Price)
                    .HasColumnType("numeric(10,2)"); 
            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.ToTable("lessons");

                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(x => x.Course)
                    .WithMany(x => x.Lessons)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // M : N relationship between User and Course via Enrollment
            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.ToTable("enrollments");

                entity.HasKey(x => new { x.UserId, x.CourseId });

                entity.Property(x => x.EnrolledAt)
                    .HasDefaultValueSql("now()");

                entity.HasOne(x => x.User)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.Course)
                    .WithMany(x => x.Enrollments)
                    .HasForeignKey(x => x.CourseId);
            });
        }
    }

}
