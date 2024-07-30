var builder = WebApplication.CreateBuilder(args);

builder
    .AddCommonFeatures()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();
await app.RunAsync();
