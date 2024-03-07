using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure;

namespace DevOpsDeploy.Application.Releases.Queries;

public class GetReleasesQuery
{
    public List<Release> Handle(int keep)
    {
        var environments = InMemoryRepository.Environments();
        var projects = InMemoryRepository.Projects();
        
        List<Release> releasesToKeep = [];
        
        foreach (var env in environments)
        {
            var envDeployments = InMemoryRepository.Deployments().Where(deployment => deployment.EnvironmentId.Equals(env.Id)).ToList();
            foreach (var project in projects)
            {
                var projectReleases = InMemoryRepository.Releases().Where(release => release.ProjectId.Equals(project.Id)).ToList();
                var projectDeployments = 
                    envDeployments.Where(deployment =>
                        projectReleases.Any(release => release.Id.Equals(deployment.ReleaseId)))
                    .OrderBy(deployment => deployment.DeployedAt)
                    .ToList();
    
                var distinctProjectDeployments = projectDeployments
                    .GroupBy(deployment => deployment.ReleaseId)
                    .Select(group => group.First())
                    .ToList();

                var projectDeploymentsToKeep = distinctProjectDeployments.Take(keep).ToList();
        
                foreach (var deploy in projectDeploymentsToKeep)
                {
                    var release = projectReleases.FirstOrDefault(release => release.Id.Equals(deploy.ReleaseId))!;
                    releasesToKeep.Add(release);
                }
            }
        }

        var result = releasesToKeep.GroupBy(release => release.Id).Select(group => group.First()).ToList();
        return result;
    }
}