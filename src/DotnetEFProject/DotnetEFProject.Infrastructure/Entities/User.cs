using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public UserProfile? Profile { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = [];
    }

}
