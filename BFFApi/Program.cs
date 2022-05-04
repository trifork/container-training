using BFFApi;
using BFFApi.HealthChecks;
using BFFBillingApi.Client;
using BFFConsumptionApi.Client;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Shared.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddCheck<HealthCheckConsumptionApi>("ConsumptionApi")
    .AddCheck<HealthCheckBillingApi>("BillingApi")
    .AddCheck<StartupHealthCheck>(
        "Startup",
        tags: new[] { "ready" });
;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging(builder => builder.AddSeq("http://log:5341"));
builder.Services.AddSingleton<StartupHealthCheck>();
builder.Services.AddHostedService<StartupBackgroundService>();  

var billingPort = Environment.GetEnvironmentVariable("BILLING_PORT");
var billingEndpoint = $"http://bffbillingapi:{billingPort}";
Console.WriteLine($"Billing endpoint: {billingEndpoint}");
builder.Services.AddHttpClient(typeof(BillingApiClient).FullName, x => x.BaseAddress = new Uri(billingEndpoint));

var consumptionPort = Environment.GetEnvironmentVariable("CONSUMPTION_PORT");
var consumptionEndpoint = $"http://bffconsumptionapi:{consumptionPort}";
Console.WriteLine($"Consumption endpoint: {consumptionEndpoint}");
builder.Services.AddHttpClient(typeof(ConsumptionClient).FullName, x =>x.BaseAddress = new Uri(consumptionEndpoint));

builder.Services.AddTransient<IBillingApiClient>(serviceProvider =>
{
    var httpclientFactory = serviceProvider.GetService<IHttpClientFactory>();

    return new BillingApiClient(billingEndpoint, httpclientFactory.CreateClient(typeof(BillingApiClient).FullName));
});

builder.Services.AddTransient<IConsumptionClient>(serviceProvider =>
{
    var httpclientFactory = serviceProvider.GetService<IHttpClientFactory>();
    return new ConsumptionClient(consumptionEndpoint, httpclientFactory.CreateClient(typeof(ConsumptionClient).FullName));
});

var app = builder.Build();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("ready")
});

if (app.Environment.IsDevelopment())
{
    
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();

app.MapControllers();

app.Run();
