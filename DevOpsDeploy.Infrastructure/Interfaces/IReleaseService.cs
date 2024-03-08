using DevOpsDeploy.Domain.Entities;

namespace DevOpsDeploy.Infrastructure.Interfaces;

public interface IReleaseService
{
    List<Release> GetReleasesByProject(string projectId);
}