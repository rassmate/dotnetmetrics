using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
var meters = new OtelMetrics();
builder.Services.AddSingleton<OtelMetrics>(meters);

builder.Services
    .AddControllers();

builder.Services
    .AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder =>
                tracerProviderBuilder
                    .AddSource(DiagnosticsConfig.ActivitySource.Name)
                    .ConfigureResource(resource => resource
                        .AddService(DiagnosticsConfig.ServiceName))
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter())
    .WithMetrics(opts =>
        opts
         .ConfigureResource(resource => resource.AddService(DiagnosticsConfig.ServiceName))
         .AddMeter(meters.MetricName)
         .AddAspNetCoreInstrumentation()
         .AddRuntimeInstrumentation()         
         .AddProcessInstrumentation()
         .AddOtlpExporter()
    );



var app = builder.Build();

app.MapControllers();

app.Run();

public static class DiagnosticsConfig
{
    public const string ServiceName = "test";
    public static ActivitySource ActivitySource = new ActivitySource(ServiceName);
}
