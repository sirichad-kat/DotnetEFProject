using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Entities;
using DotnetEFProject.Infrastructure.Mappings.CourseEnrollment;
using DotnetEFProject.Infrastructure.Postgres.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment
{ 

    public interface IDeleteUserRepository
    {
        Task DeleteUserAsync(DeleteUserInput input, CancellationToken ct = default);
    }

    public class DeleteUserRepository : IDeleteUserRepository
    {
        readonly PostgresApplicationDbContext context;
        public DeleteUserRepository(PostgresApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task DeleteUserAsync(DeleteUserInput input, CancellationToken ct = default)
        {
            var entity = await context.Users
                .Include(u => u.UserProfile)
                .Include(u => u.Enrollments)
                .FirstOrDefaultAsync(u => u.Id == input.Id, ct);

            if (entity is null) return;

            if (entity.UserProfile is not null)
                context.UserProfiles.Remove(entity.UserProfile);

            if (entity.Enrollments.Count > 0)
                context.Enrollments.RemoveRange(entity.Enrollments);

            context.Users.Remove(entity);
            await context.SaveChangesAsync(ct);
        }
    }
}
