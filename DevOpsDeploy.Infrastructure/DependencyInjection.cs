using DevOpsDeploy.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
namespace DevOpsDeploy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<DeploymentService>();
        services.AddScoped<EnvironmentService>();
        services.AddScoped<ProjectService>();
        services.AddScoped<ReleaseService>();
        return services;
    }
}