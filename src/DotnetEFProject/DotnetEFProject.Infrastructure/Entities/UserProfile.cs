namespace DotnetEFProject.Infrastructure.Entities;

public partial class UserProfile
{
    public int Id { get; set; }

    public string? Bio { get; set; }

    public DateOnly? BirthDate { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
