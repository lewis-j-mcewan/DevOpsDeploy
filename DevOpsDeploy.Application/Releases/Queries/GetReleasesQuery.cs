using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.Infrastructure.Interfaces;
using MediatR;
using Serilog;

namespace DevOpsDeploy.Application.Releases.Queries;

public class GetReleasesQuery : IRequest<List<Release>>
{
    public int Keep { get; set; }

    public class Handler : IRequestHandler<GetReleasesQuery, List<Release>>
    {
        private readonly IDeploymentService _deploymentService;
        private readonly IEnvironmentService _environmentService;
        private readonly IProjectService _projectService;
        private readonly IReleaseService _releaseService;

        public Handler(
            IDeploymentService deploymentService,
            IEnvironmentService environmentService, 
            IProjectService projectService,
            IReleaseService releaseService)
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

            if (environments is null) throw new ApplicationException("There are no environments");
            if (projects is null) throw new ApplicationException("There are no projects");

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
                    
                    //one project may have multiple deployments of the same release
                    var distinctProjectDeployments = projectDeployments
                        .GroupBy(deployment => deployment.ReleaseId)
                        .Select(group => group.First())
                        .ToList();

                    var projectDeploymentsToKeep = distinctProjectDeployments.Take(request.Keep).ToList();

                    var currentComboReleases = "";
                    foreach (var deploy in projectDeploymentsToKeep)
                    {
                        var release = projectReleases.FirstOrDefault(release => release.Id.Equals(deploy.ReleaseId))!;
                        releasesToKeep.Add(release);
                        currentComboReleases += release.Id+" | ";
                    }
                    Log.Information($"{request.Keep} most recent releases of {project.Id} to {env.Id}: {currentComboReleases}");
                }
            }
            
            var result = releasesToKeep.GroupBy(release => release.Id).Select(group => group.First()).ToList();
            LogReleasesResult(result);
            return Task.FromResult(result);
        }
        
        private static void LogReleasesResult(List<Release> releases)
        {
            var output = releases.Aggregate("", (current, release) => current + $"{release.Id} ");
            Log.Information($"Distinct Releases: {output}");
        }
    }

    
}