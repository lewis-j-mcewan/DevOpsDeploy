using DevOpsDeploy.Domain.Entities;

namespace DevOpsDeploy.Infrastructure.Services;

public class ReleaseService
{
    public List<Release> GetReleasesByProject(string projectId)
    {
        return InMemoryRepository.Releases().Where(release => release.ProjectId.Equals(projectId)).ToList();
    }
}