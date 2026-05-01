using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Backend.Tests;

public class ApiStartupTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ApiStartupTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task RootEndpoint_ReturnsSuccessAndCorrectContract()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var content = await response.Content.ReadFromJsonAsync<dynamic>();
        Assert.NotNull(content);
        string name = content?.GetProperty("name").GetString();
        Assert.False(string.IsNullOrEmpty(name));
    }

    [Fact]
    public async Task Swagger_IsAccessibleInDevelopment()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/swagger/index.html");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}