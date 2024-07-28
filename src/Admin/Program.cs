var builder = WebApplication.CreateBuilder(args);

builder
    .AddCommonFeatures()
    .AddRepositories()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

await app.RunAsync();
