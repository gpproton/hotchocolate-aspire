using hotchocolate_aspire.ApiService2;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedis("cache");

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .InitializeOnStartup()
    .PublishSchemaDefinition(c => c
    .SetName("apiservice2")
    .IgnoreRootTypes()
    .PublishToRedis("Demo", sp => sp.GetRequiredService<IConnectionMultiplexer>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();
app.MapGet("/", () => "Hello from ApiService2!");
app.MapGraphQL();

app.Run();