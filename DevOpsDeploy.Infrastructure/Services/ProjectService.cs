using DevOpsDeploy.Domain.Entities;

namespace DevOpsDeploy.Infrastructure.Services;

public class ProjectService
{
    public List<Project> GetProjects()
    {
        return InMemoryRepository.Projects();
    }
}