using DotnetEFProject.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment
{ 

    public interface IGetCourseViewReader
    {
        Task<List<GetCourseListOutput>> GetCourseListAsync(CancellationToken ct = default);
    }

    public class GetCourseViewReader : IGetCourseViewReader
    {

        //readonly PostgresApplicationDbContext context;
        //public GetLessonViewReader(PostgresApplicationDbContext _context)
        //{
        //    context = _context;
        //}

        public async Task<List<GetCourseListOutput>> GetCourseListAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
