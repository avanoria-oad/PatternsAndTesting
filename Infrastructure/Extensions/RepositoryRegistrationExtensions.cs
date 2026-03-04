using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Extensions;

public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(env);

        

        return services;
    }
}
