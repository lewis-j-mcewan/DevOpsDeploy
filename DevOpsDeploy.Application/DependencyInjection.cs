using DevOpsDeploy.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DevOpsDeploy.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddInfrastructureServices();
        return services;
    }
}