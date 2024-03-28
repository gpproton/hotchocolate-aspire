using hotchocolate_aspire.ServiceDefaults;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedis("cache");

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddHttpClient(WellKnownSchemaNames.ApiService, c => c.BaseAddress = new Uri("http://apiservice/graphql"));
builder.Services.AddHttpClient(WellKnownSchemaNames.ApiService2, c => c.BaseAddress = new Uri("http://apiservice2/graphql"));

builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddRemoteSchemasFromRedis("Demo", sp => sp.GetRequiredService<IConnectionMultiplexer>());

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();
app.MapGet("/", () => "Hello from Gateway!");
app.MapGraphQL();

app.Run();
