using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;

namespace DevOpsDeploy.Infrastructure.Services;

public class ReleaseService : IReleaseService
{
    private readonly DatabaseSettings _dbSettings;
    
    public ReleaseService(IOptions<DatabaseSettings> dbOptions)
    {
        _dbSettings = dbOptions.Value;
    }
    public List<Release> GetReleasesByProject(string projectId)
    {
        var releases = InMemoryRepository.Releases(_dbSettings.ConnectionString);
        return (releases is null) ? [] : releases.Where(release => release.ProjectId.Equals(projectId)).ToList();
    }
}