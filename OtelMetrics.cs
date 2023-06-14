using System.Collections.Generic;
using System.Diagnostics.Metrics;


public class OtelMetrics
{
    //Forecasts meters
    private  Counter<int> ForeCastsRetrieved { get; }   

    public string MetricName { get; }

    public OtelMetrics(string meterName = "Forecasts")
    {
        var meter = new Meter(meterName);
        MetricName = meterName;

        ForeCastsRetrieved = meter.CreateCounter<int>("forecasts-retrieved", "Forecast");
    }

    //Forecasts meters
    public void IncreaseForecastsRetrieved() => ForeCastsRetrieved.Add(1);
}