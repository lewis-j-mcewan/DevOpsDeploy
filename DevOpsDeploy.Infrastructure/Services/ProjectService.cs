using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;

namespace DevOpsDeploy.Infrastructure.Services;

public class ProjectService : IProjectService
{
    public List<Project> GetProjects()
    {
        return InMemoryRepository.Projects()?? [];
    }
}