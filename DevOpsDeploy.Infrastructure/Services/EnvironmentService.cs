using Environment = DevOpsDeploy.Domain.Entities.Environment;

namespace DevOpsDeploy.Infrastructure.Services;

public class EnvironmentService
{
    public List<Environment> GetEnvironments()
    {
        return InMemoryRepository.Environments();
    }
}