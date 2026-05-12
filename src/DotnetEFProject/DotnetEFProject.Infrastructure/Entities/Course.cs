namespace DotnetEFProject.Infrastructure.Entities;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
