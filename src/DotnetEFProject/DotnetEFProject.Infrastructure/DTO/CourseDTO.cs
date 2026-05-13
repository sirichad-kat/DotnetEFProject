using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.DTO
{

    public sealed record AddCourseInput(string Title, decimal Price, ICollection<LessonInput> Lessons);

    public sealed record LessonInput( string Title, int DurationMinutes, int CourseId  );
     

    public sealed record GetCourseListOutput(string Title, decimal Price, ICollection<LessonOutput> Lessons);


    public sealed record LessonOutput(string Title, int DurationMinutes);
}
