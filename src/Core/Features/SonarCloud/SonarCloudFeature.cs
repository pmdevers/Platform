using Featurize.AspNetCore;
using SonarCloud.NET;
using SonarCloud.NET.Extensions;

namespace FinSecure.Platform.Core.Features.SonarCloud;

public class SonarCloudFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddSonarCloudClient(x=> x.AccessToken = "testToken");
    }

    public void Use(WebApplication app)
    {
        app.MapCreateProject();
    }
}
