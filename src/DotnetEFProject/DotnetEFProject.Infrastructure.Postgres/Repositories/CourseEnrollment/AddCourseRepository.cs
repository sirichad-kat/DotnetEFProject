using DotnetEFProject.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment
{
     

    public interface IAddCourseRepository
    {
        Task AddLessonAsync(AddCourseInput input, CancellationToken ct = default);
    }

    public class AddCourseRepository : IAddCourseRepository
    {
        //readonly PostgresApplicationDbContext context;
        //public AddLessonRepository(PostgresApplicationDbContext _context)
        //{
        //    context = _context;
        //}

        public async Task AddLessonAsync(AddCourseInput input, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
