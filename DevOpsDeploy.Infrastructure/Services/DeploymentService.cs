using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;

namespace DevOpsDeploy.Infrastructure.Services;

public class DeploymentService : IDeploymentService
{
    public List<Deployment> GetDeploymentsByEnv(string envId)
    {
        var deployments = InMemoryRepository.Deployments();
        return (deployments is null) ? [] : deployments.Where(dep => dep.EnvironmentId.Equals(envId)).ToList();
    }
}