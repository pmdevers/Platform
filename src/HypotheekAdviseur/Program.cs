var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddCommonFeatures()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

await app.RunAsync();
