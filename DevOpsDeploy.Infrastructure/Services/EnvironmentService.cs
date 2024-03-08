using DevOpsDeploy.Infrastructure.Interfaces;
using Environment = DevOpsDeploy.Domain.Entities.Environment;

namespace DevOpsDeploy.Infrastructure.Services;

public class EnvironmentService : IEnvironmentService
{
    public List<Environment> GetEnvironments()
    {
        return InMemoryRepository.Environments()?? [];
    }
}