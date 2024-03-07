namespace DevOpsDeploy.Domain.Entities;

public class Deployment
{
    public string Id { get; set; } = string.Empty;
    public string ReleaseId { get; set; } = string.Empty;
    public string EnvironmentId { get; set; } = string.Empty;
    public DateTime DeployedAt { get; set; }
}