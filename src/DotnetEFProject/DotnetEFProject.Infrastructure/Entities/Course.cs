using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }

        public ICollection<Lesson> Lessons { get; set; } = [];
        public ICollection<Enrollment> Enrollments { get; set; } = [];
    }


}
