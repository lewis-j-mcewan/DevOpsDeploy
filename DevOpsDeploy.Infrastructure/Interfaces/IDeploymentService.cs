using DevOpsDeploy.Domain.Entities;

namespace DevOpsDeploy.Infrastructure.Interfaces;

public interface IDeploymentService
{
    List<Deployment> GetDeploymentsByEnv(string envId);
}