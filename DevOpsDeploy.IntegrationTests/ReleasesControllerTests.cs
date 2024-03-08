using DevOpsDeploy.Domain.Entities;
using DevOpsDeploy.IntegrationTests.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DevOpsDeploy.IntegrationTests;

public class ReleasesControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ReleasesControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async void GivenKeep0_WhenGettingReleases_ThenReturn0Releases()
    {
        //Arrange
        HttpClient client = _factory.CreateClient();
        var url = $"{client.BaseAddress}Releases?keep=0";
    
        //Act
        HttpResponseMessage responseMessage = await client.GetAsync(url);
    
        //Assert
        responseMessage.EnsureSuccessStatusCode();
        var releases = await responseMessage.DeserializeContentAsync<List<Release>>();
        releases.Should().HaveCount(0);
    }
    
    [Fact]
    public async void GivenKeep1_WhenGettingReleases_ThenReturn3Releases()
    {
        //Arrange
        HttpClient client = _factory.CreateClient();
        var url = $"{client.BaseAddress}Releases?keep=1";

        //Act
        HttpResponseMessage responseMessage = await client.GetAsync(url);

        //Assert
        responseMessage.EnsureSuccessStatusCode();
        var releases = await responseMessage.DeserializeContentAsync<List<Release>>();
        releases.Should().HaveCount(3);
        releases.Should().Contain(release => release.Id.Equals("Release-1"));
    }
    
    [Fact]
    public async void GivenKeep3_WhenGettingReleases_ThenReturn5Releases()
    {
        //Arrange
        HttpClient client = _factory.CreateClient();
        var url = $"{client.BaseAddress}Releases?keep=3";

        //Act
        HttpResponseMessage responseMessage = await client.GetAsync(url);

        //Assert
        responseMessage.EnsureSuccessStatusCode();
        var releases = await responseMessage.DeserializeContentAsync<List<Release>>();
        releases.Should().HaveCount(5);
        releases.Should().Contain(release => release.Id.Equals("Release-7"));
    }
}