var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache").WithRedisCommander();

var apiService = builder.AddProject<Projects.hotchocolate_aspire_ApiService>("apiservice").WithReference(cache);
var apiService2 = builder.AddProject<Projects.hotchocolate_aspire_ApiService2>("apiservice2").WithReference(cache);
var apiGateway = builder.AddProject<Projects.hotchocolate_aspire_ApiGateway>("apigateway").WithReference(cache);


builder.AddProject<Projects.hotchocolate_aspire_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService)
    .WithReference(apiService2)
    .WithReference(apiGateway);

builder.Build().Run();
