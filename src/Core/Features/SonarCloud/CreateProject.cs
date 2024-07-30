using Microsoft.AspNetCore.Hosting.Server;
using MinimalKafka;
using MinimalKafka.Extension;
using SonarCloud.NET;


namespace FinSecure.Platform.Core.Features.SonarCloud;

public static class CreateProject
{
    public static void MapCreateProject(this WebApplication builder)
    {
        builder.MapTopic("project", HandleAsync)
            .WithGroupId("sonarcloud");
    }

    public static async Task HandleAsync(string value, ISonarCloudApiClient client)
    {
        await client.Projects.Create(new() 
        { 
            Organization = "tjip",
            Name = value, 
            Project = value
        });
    }
}
