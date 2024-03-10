using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace DevOpsDeploy.Infrastructure.Services;

public class DeploymentService : IDeploymentService
{
    private readonly DatabaseSettings _dbSettings;
    // private InMemoryRepository _inMemoryRepository;
    
    public DeploymentService(IOptions<DatabaseSettings> dbOptions)
    {
        _dbSettings = dbOptions.Value;
        // _inMemoryRepository = new InMemoryRepository(_dbSettings.ConnectionString);
    }
    public List<Deployment> GetDeploymentsByEnv(string envId)
    {
        var deployments = InMemoryRepository.Deployments(_dbSettings.ConnectionString);
        return (deployments is null) ? [] : deployments.Where(dep => dep.EnvironmentId.Equals(envId)).ToList();
    }
}