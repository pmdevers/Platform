using Featurize.AspNetCore;
using FinSecure.Platform.Common.OpenApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace FinSecure.Platform.Common.OpenApi;

public class OpenApiFeature : IWebApplicationFeature
{
    private readonly string _company;
    private readonly string _product;

    public OpenApiFeature()
    {
        var assembly = Assembly.GetEntryAssembly();

        _company = assembly?.GetCustomAttribute<AssemblyCompanyAttribute>()?.Company ?? string.Empty;
        _product = assembly?.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? string.Empty;
    }

    public void Configure(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            OpenApiInfo apiinfo = new()
            {
                Title = _product,
                Version = "v1",
                Description = $"Api's for {_company}."
            };

            options.SchemaFilter<EnumSchemaFilter>();
            options.MapValueObjects();

            options.SwaggerDoc("v1", apiinfo);
        });
    }

    public void Use(WebApplication app)
    {
        app.UseSwagger(x => x.RouteTemplate = "api-docs/{documentName}/swagger.json");
        app.UseSwaggerUI(c =>
        {

            c.SwaggerEndpoint("/api-docs/v1/swagger.json", _company);
        });
    }
}
