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
    public interface IModifyUserRepository
    {
        Task ModifyUserAsync(ModifyUserInput req, CancellationToken ct = default);
    }

    public class ModifyUserRepository : IModifyUserRepository
    {
        readonly PostgresApplicationDbContext context;
        public ModifyUserRepository(PostgresApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task ModifyUserAsync(ModifyUserInput input, CancellationToken ct = default)
        {
            var user = await context.Users
                .Include(u => u.UserProfile)
                .FirstOrDefaultAsync(u => u.Id == input.Id, ct);

            if (user is null) return;

            if (input.Email is not null) user.Email = input.Email;

            // Update or create UserProfile
            if (input.Bio is not null)
            {
                if (user.UserProfile is null)
                {
                    user.UserProfile = new UserProfile
                    {
                        UserId = user.Id,
                        Bio = input.Bio, 
                    };
                }
                else
                {
                    user.UserProfile.Bio = input.Bio; 
                }
            }

            await context.SaveChangesAsync(ct);
        }
    }
}
