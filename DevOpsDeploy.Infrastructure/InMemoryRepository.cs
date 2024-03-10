using DevOpsDeploy.Domain.Entities;
using Newtonsoft.Json;
using Environment = DevOpsDeploy.Domain.Entities.Environment;

namespace DevOpsDeploy.Infrastructure;

public static class InMemoryRepository
{
    public static List<Deployment>? Deployments(string connectionString)
    {
        var json = File.ReadAllText(connectionString + "Deployments.json");
        var deployments = JsonConvert.DeserializeObject<List<Deployment>>(json)!;
        return deployments;
    }
    
    public static List<Environment>? Environments(string connectionString)
    {
        var json = File.ReadAllText(connectionString + "Environments.json");
        var environments = JsonConvert.DeserializeObject<List<Environment>>(json)!;
        return environments;
    }
    
    public static List<Project>? Projects(string connectionString)
    {
        var json = File.ReadAllText(connectionString + "Projects.json");
        var projects = JsonConvert.DeserializeObject<List<Project>>(json)!;
        return projects;
    }
    
    public static List<Release>? Releases(string connectionString)
    {
        var json = File.ReadAllText(connectionString + "Releases.json");
        var releases = JsonConvert.DeserializeObject<List<Release>>(json)!;
        return releases;
    }
}