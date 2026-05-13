using DotnetEFProject.Infrastructure.Postgres.Queries.CourseEnrollment;
using DotnetEFProject.Infrastructure.Postgres.Repositories.CourseEnrollment;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetEFProject.Api.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IGetUserListViewReader, GetUserListViewReader>();
            services.AddScoped<IGetUserViewReader, GetUserViewReader>();
            services.AddScoped<IAddUserRepository, AddUserRepository>();

            return services;
        }
    }
}
