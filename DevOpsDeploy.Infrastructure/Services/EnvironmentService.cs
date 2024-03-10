using DevOpsDeploy.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Environment = DevOpsDeploy.Domain.Entities.Environment;

namespace DevOpsDeploy.Infrastructure.Services;

public class EnvironmentService : IEnvironmentService
{
    private readonly DatabaseSettings _dbSettings;

    public EnvironmentService(IOptions<DatabaseSettings> dbOptions)
    {
        _dbSettings = dbOptions.Value;
    }
    
    public List<Environment> GetEnvironments()
    {
        return InMemoryRepository.Environments(_dbSettings.ConnectionString)?? [];
    }
}