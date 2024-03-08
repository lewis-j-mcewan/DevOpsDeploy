using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;

namespace DevOpsDeploy.Infrastructure.Services;

public class ReleaseService : IReleaseService
{
    public List<Release> GetReleasesByProject(string projectId)
    {
        var releases = InMemoryRepository.Releases();
        return (releases is null) ? [] : releases.Where(release => release.ProjectId.Equals(projectId)).ToList();
    }
}