using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace DevOpsDeploy.Infrastructure.Services;

public class ProjectService : IProjectService
{
    private readonly DatabaseSettings _dbSettings;

    public ProjectService(IOptions<DatabaseSettings> dbOptions)
    {
        _dbSettings = dbOptions.Value;
    }
    
    public List<Project> GetProjects()
    {
        return InMemoryRepository.Projects(_dbSettings.ConnectionString)?? [];
    }
}