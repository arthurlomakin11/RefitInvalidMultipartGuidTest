using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Refit;
using WebApplication1.Interfaces;
using Xunit;

namespace WebApplication1.Tests;

public class UploadTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly IUploadApi _client;

    public UploadTests(WebApplicationFactory<Program> factory)
    {
        var handler = new LoggingHandler(new HttpClientHandler());
        
        var httpClient = factory.CreateDefaultClient(handler);
        
        _client = RestService.For<IUploadApi>(httpClient, new RefitSettings
        {
            ContentSerializer = new ExtendedHttpContentSerializer(new SystemTextJsonContentSerializer())
        });
    }

    [Fact]
    public async Task Upload_File_Success()
    {
        // Arrange
        var id = Guid.NewGuid();
        var fileContent = new MemoryStream("Test file"u8.ToArray());
        var file = new StreamPart(fileContent, "test.txt", "text/plain");
        var startDate = new DateOnly(2022, 1, 1);

        // Act
        var response = await _client.UploadAsync(id, startDate, file);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(id.ToString(), response.Content!.ToString());
    }
}