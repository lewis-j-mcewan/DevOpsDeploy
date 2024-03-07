namespace DevOpsDeploy.Domain.Entities;

public class Release
{
    public string Id { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}