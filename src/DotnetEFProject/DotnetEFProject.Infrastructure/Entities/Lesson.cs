namespace DotnetEFProject.Infrastructure.Entities;

public partial class Lesson
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}
