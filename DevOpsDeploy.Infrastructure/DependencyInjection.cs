using DevOpsDeploy.Infrastructure.Interfaces;
using DevOpsDeploy.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
namespace DevOpsDeploy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDeploymentService, DeploymentService>();
        services.AddScoped<IEnvironmentService, EnvironmentService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IReleaseService, ReleaseService>();
        return services;
    }
}