using Microsoft.AspNetCore.Http.HttpResults;

namespace FinSecure.Platform.HypotheekAdviseur.Features.Core;

public static class Landing
{
    public static void MapLandingPage(this IEndpointRouteBuilder builder) 
        => builder.MapGet("/", () => new RazorComponentResult<MainPage>());
}
