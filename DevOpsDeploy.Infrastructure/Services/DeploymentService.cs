using DevOpsDeploy.Domain.Entities;

namespace DevOpsDeploy.Infrastructure.Services;

public class DeploymentService
{
    public List<Deployment> GetDeploymentsByEnv(string envId)
    {
        return InMemoryRepository.Deployments().Where(dep => dep.EnvironmentId.Equals(envId)).ToList();
    }
}