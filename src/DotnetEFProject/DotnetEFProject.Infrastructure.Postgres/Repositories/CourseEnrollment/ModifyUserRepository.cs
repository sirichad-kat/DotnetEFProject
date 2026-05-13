using DotnetEFProject.Infrastructure.DTO;

namespace DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment
{ 
    public interface IModifyUserRepository
    {
        Task ModifyUserAsync(ModifyUserInput req, CancellationToken ct = default);
    }

    public class ModifyUserRepository : IModifyUserRepository
    {
        //readonly PostgresApplicationDbContext context;
        //public ModifyUserRepository(PostgresApplicationDbContext _context)
        //{
        //    context = _context;
        //}

        public async Task ModifyUserAsync(ModifyUserInput input, CancellationToken ct = default)
        { 
            throw new NotImplementedException();
        }
    }
}
