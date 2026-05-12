using System;
using System.Collections.Generic;

namespace DotnetEFProject.Infrastructure.Postgres.Persistence;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual UserProfile? UserProfile { get; set; }
}
