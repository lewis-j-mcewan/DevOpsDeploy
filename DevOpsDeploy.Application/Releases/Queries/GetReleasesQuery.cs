using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Services;
using MediatR;

namespace DevOpsDeploy.Application.Releases.Queries;

public class GetReleasesQuery : IRequest<List<Release>>
{
    public int Keep { get; set; }

    public class Handler : IRequestHandler<GetReleasesQuery, List<Release>>
    {
        private readonly DeploymentService _deploymentService;
        private readonly EnvironmentService _environmentService;
        private readonly ProjectService _projectService;
        private readonly ReleaseService _releaseService;

        public Handler(
            DeploymentService deploymentService,
            EnvironmentService environmentService, 
            ProjectService projectService,
            ReleaseService releaseService)
        {
            _deploymentService = deploymentService;
            _environmentService = environmentService;
            _projectService = projectService;
            _releaseService = releaseService;
        }
        public Task<List<Release>> Handle(GetReleasesQuery request, CancellationToken cancellationToken)
        {
            var environments = _environmentService.GetEnvironments();
            var projects = _projectService.GetProjects();
        
            List<Release> releasesToKeep = [];
        
            foreach (var env in environments)
            {
                var envDeployments = _deploymentService.GetDeploymentsByEnv(env.Id);
                foreach (var project in projects)
                {
                    var projectReleases = _releaseService.GetReleasesByProject(project.Id);
                    var projectDeployments = 
                        envDeployments.Where(deployment =>
                                projectReleases.Any(release => release.Id.Equals(deployment.ReleaseId)))
                            .OrderBy(deployment => deployment.DeployedAt)
                            .ToList();
    
                    var distinctProjectDeployments = projectDeployments
                        .GroupBy(deployment => deployment.ReleaseId)
                        .Select(group => group.First())
                        .ToList();

                    var projectDeploymentsToKeep = distinctProjectDeployments.Take(request.Keep).ToList();
        
                    foreach (var deploy in projectDeploymentsToKeep)
                    {
                        var release = projectReleases.FirstOrDefault(release => release.Id.Equals(deploy.ReleaseId))!;
                        releasesToKeep.Add(release);
                    }
                }
            }

            var result = releasesToKeep.GroupBy(release => release.Id).Select(group => group.First()).ToList();
            return Task.FromResult(result);
        }
    }
}