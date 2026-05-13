using Dapper;
using DotnetEFProject.Infrastructure.DTO;
using DotnetEFProject.Infrastructure.Postgres.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment
{
    public interface IGetUserListViewReader
    {
        Task<List<GetUserListOutput>> GetUserListViewAsync(CancellationToken ct = default);
    }

    public class GetUserListViewReader : IGetUserListViewReader
    {

        readonly PostgresApplicationDbContext context;
        public GetUserListViewReader(PostgresApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task<List<GetUserListOutput>> GetUserListViewAsync(CancellationToken ct = default)
        {
            var connection = context.Database.GetDbConnection();

            var sql = """
                SELECT
                    u.id        AS Id,
                    u.full_name AS FullName,
                    up.birth_date AS BirthDate
                FROM users u
                LEFT JOIN user_profiles up ON up.user_id = u.id
                """;

            var result = await connection.QueryAsync<GetUserListOutput>(
                new CommandDefinition(sql, cancellationToken: ct));

            return result.AsList();
        }
    }

}
