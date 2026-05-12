using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Entities
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int DurationMinutes { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }

}
