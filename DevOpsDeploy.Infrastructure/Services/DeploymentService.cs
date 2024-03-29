using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace DevOpsDeploy.Infrastructure.Services;

public class DeploymentService : IDeploymentService
{
    private readonly DatabaseSettings _dbSettings;
    
    public DeploymentService(IOptions<DatabaseSettings> dbOptions)
    {
        _dbSettings = dbOptions.Value;
    }
    public List<Deployment> GetDeploymentsByEnv(string envId)
    {
        var deployments = InMemoryRepository.Deployments(_dbSettings.ConnectionString);
        return (deployments is null) ? [] : deployments.Where(dep => dep.EnvironmentId.Equals(envId)).ToList();
    }
}