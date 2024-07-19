using Featurize.AspNetCore;

namespace FinSecure.Platform.HypotheekAdviseur.Features.Core;

public class CoreFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {
        services.AddRazorComponents();
    }

    public void Use(WebApplication app)
    {
        app.MapLandingPage();
    }
}