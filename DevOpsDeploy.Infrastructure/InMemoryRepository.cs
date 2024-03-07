using DevOpsDeploy.Domain.Entities;
using Newtonsoft.Json;
using Environment = DevOpsDeploy.Domain.Entities.Environment;

namespace DevOpsDeploy.Infrastructure;

public static class InMemoryRepository
{
    public static List<Deployment> Deployments()
    {
        var json = File.ReadAllText("../DevOpsDeploy.Infrastructure/Repository/Deployments.json");
        var deployments = JsonConvert.DeserializeObject<List<Deployment>>(json)!;
        return deployments;
    }
    
    public static List<Environment> Environments()
    {
        var json = File.ReadAllText("../DevOpsDeploy.Infrastructure/Repository/Environments.json");
        var environments = JsonConvert.DeserializeObject<List<Environment>>(json)!;
        return environments;
    }
    
    public static List<Project> Projects()
    {
        var json = File.ReadAllText("../DevOpsDeploy.Infrastructure/Repository/Projects.json");
        var projects = JsonConvert.DeserializeObject<List<Project>>(json)!;
        return projects;
    }
    
    public static List<Release> Releases()
    {
        var json = File.ReadAllText("../DevOpsDeploy.Infrastructure/Repository/Releases.json");
        var releases = JsonConvert.DeserializeObject<List<Release>>(json)!;
        return releases;
    }
}