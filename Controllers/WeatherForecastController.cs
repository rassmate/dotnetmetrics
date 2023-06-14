using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Configuration;

namespace docker_dotnetcore_api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly OtelMetrics otelMetrics;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, OtelMetrics otelMetrics)
    {
        _logger = logger;
        this.otelMetrics = otelMetrics;
    }

    [HttpGet("/v1/inanotherservice")]
    public IEnumerable<WeatherForecast> Get()
    {
        
        otelMetrics.IncreaseForecastsRetrieved();
        _logger.Log(LogLevel.Information, "forecasts retrieved");
           
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
