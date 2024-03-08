using Environment = DevOpsDeploy.Domain.Entities.Environment;

namespace DevOpsDeploy.Infrastructure.Interfaces;

public interface IEnvironmentService
{
    List<Environment> GetEnvironments();
}