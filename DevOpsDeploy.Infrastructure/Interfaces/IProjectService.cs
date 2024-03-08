using DevOpsDeploy.Domain.Entities;

namespace DevOpsDeploy.Infrastructure.Interfaces;

public interface IProjectService
{
    List<Project> GetProjects();
}