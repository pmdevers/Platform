using Featurize.AspNetCore;
using FinSecure.Platform.Common.Storage;

namespace FinSecure.Platform.Core.Features.Projects;

public class ProjectsFeature : IUseFeature
{ 

    public void Use(WebApplication app)
    {
       var group = app.MapGroup("/v1/projects")     
            .WithTags("Projects");

        group.MapGetProject();
        group.MapCreateProject();
        group.MapDeleteProject();
    }
}
