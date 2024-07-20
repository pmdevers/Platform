using Featurize.AspNetCore;
using FinSecure.Platform.HypotheekAdviseur.Components;
using FinSecure.Platform.HypotheekAdviseur.Components.Shared;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FinSecure.Platform.HypotheekAdviseur.Features;

public class CoreFeature : IWebApplicationFeature
{
    public void Configure(IServiceCollection services)
    {

        services.AddRazorComponents()
            .AddInteractiveServerComponents();

        services
            .AddSingleton<HtmxCounter.HtmxCounterState>();
    }

    public void Use(WebApplication app)
    {
        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.MapPost("/count", (HtmxCounter.HtmxCounterState value) =>
        {
            value.Value++;
            return new RazorComponentResult<HtmxCounter>(
                new { State = value }
            );
        });
    }


}