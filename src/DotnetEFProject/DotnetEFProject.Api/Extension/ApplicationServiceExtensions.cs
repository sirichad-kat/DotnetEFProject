using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DotnetEFProject.Api.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, Assembly? assembly = null)
        {
            assembly ??= Assembly.GetExecutingAssembly();

            services.Scan(scan => scan
               .FromAssemblies(assembly)
               .AddClasses(classes => classes
                   .Where(type => type.Name.EndsWith("Service")))
               .AsSelfWithInterfaces()
               .WithScopedLifetime());
            return services;
        }
    }
}
