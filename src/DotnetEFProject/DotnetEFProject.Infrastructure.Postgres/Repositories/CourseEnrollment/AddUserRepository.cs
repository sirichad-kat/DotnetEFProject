using DotnetEFProject.Infrastructure.DTO;

namespace DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment
{
    public interface IAddUserRepository
    {
        Task AddUserAsync(AddUserInput req, CancellationToken ct = default);
    }

    public class AddUserRepository : IAddUserRepository
    {
        //readonly PostgresApplicationDbContext context;
        //public AddUserRepository(PostgresApplicationDbContext _context)
        //{
        //    context = _context;
        //}

        public async Task AddUserAsync(AddUserInput req, CancellationToken ct = default)
        { 
           throw new NotImplementedException();
        }
    }
}
