using System;
using System.Collections.Generic;

namespace DotnetEFProject.Infrastructure.Postgres.Persistence;

public partial class UserProfile
{
    public int Id { get; set; }

    public string? Bio { get; set; }

    public DateOnly? BirthDate { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
