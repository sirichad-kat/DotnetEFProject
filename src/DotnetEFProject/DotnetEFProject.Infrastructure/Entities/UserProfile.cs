

namespace DotnetEFProject.Infrastructure.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string? Bio { get; set; }
        public DateOnly? BirthDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
