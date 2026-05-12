using System;
using System.Collections.Generic;

namespace DotnetEFProject.Infrastructure.Postgres.Persistence;

public partial class Enrollment
{
    public int UserId { get; set; }

    public int CourseId { get; set; }

    public DateTime EnrolledAt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
