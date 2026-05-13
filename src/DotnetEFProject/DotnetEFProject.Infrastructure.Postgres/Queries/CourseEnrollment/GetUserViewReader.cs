using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Mappings.CourseEnrollment;
using DotnetEFProject.Infrastructure.Postgres.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment
{


    public interface IGetUserViewReader
    {
        Task<GetUserOutput?> GetUserAsync(GetUserInput req, CancellationToken ct = default);
    }

    public class GetUserViewReader : IGetUserViewReader
    {

        readonly PostgresApplicationDbContext context;
        public GetUserViewReader(PostgresApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<GetUserOutput?> GetUserAsync(GetUserInput req, CancellationToken ct = default)
        {
            var res = await context.Users
                   .AsNoTracking()
                   .Where(x => x.Id == req.Id)
                   .FirstOrDefaultAsync(ct);
            return res != null ? UserMapper.ToDto(res) : null;
        }
    }

}
