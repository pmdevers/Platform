var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddCommonFeatures()
    .AddRepositories()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

await app.RunAsync();
