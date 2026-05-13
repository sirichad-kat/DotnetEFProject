using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Entities;
using DotnetEFProject.Infrastructure.Mappings.CourseEnrollment;
using DotnetEFProject.Infrastructure.Postgres.Persistence;
using Microsoft.Extensions.Logging;

namespace DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment
{
    public interface IAddUserRepository
    {
        Task AddUserAsync(AddUserInput req, CancellationToken ct = default);
    }

    public class AddUserRepository : IAddUserRepository
    {
        readonly PostgresApplicationDbContext context;
        public AddUserRepository(PostgresApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddUserAsync(AddUserInput req, CancellationToken ct = default)
        { 
            User input = UserMapper.ToUserEntity(req);
            context.Users.Add(input);
            await context.SaveChangesAsync(ct); 
        }
    }
}
