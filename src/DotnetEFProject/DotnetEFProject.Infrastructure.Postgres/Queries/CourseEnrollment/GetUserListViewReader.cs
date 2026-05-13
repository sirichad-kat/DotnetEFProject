using DotnetEFProject.Infrastructure.DTO;

namespace DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment
{
    public interface IGetUserListViewReader
    {
        Task<List<GetUserListOutput>> GetUserListViewAsync(CancellationToken ct = default);
    }

    public class GetUserListViewReader : IGetUserListViewReader
    {

        //readonly PostgresApplicationDbContext context;
        //public GetUserListViewReader(PostgresApplicationDbContext _context)
        //{
        //    context = _context;
        //}

        public async Task<List<GetUserListOutput>> GetUserListViewAsync(CancellationToken ct = default)
        {
             throw new NotImplementedException();
        }
    }

}
