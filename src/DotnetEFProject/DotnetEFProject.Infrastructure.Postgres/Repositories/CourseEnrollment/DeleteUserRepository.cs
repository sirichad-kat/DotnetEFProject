using DotnetEFProject.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;

namespace DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment
{

    public interface IDeleteUserRepository
    {
        Task DeleteUserAsync(DeleteUserInput input, CancellationToken ct = default);
    }

    public class DeleteUserRepository : IDeleteUserRepository
    {
        //    readonly PostgresApplicationDbContext context;
        //    public DeleteUserRepository(PostgresApplicationDbContext _context)
        //    {
        //        context = _context;
        //    }

        public async Task DeleteUserAsync(DeleteUserInput input, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
