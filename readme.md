# HealthChecks.ClickHouse

Health check implementation for Clickhouse based on Clickhouse.Net package.

## Usage

Startup.cs
```
public void ConfigureServices(IServiceCollection services) 
{
    // ...
    services.AddClickHouse();
    services.AddTransient(p => new ClickHouseConnectionSettings(connectionString));

    services.AddHealthChecks()
                .AddClickHouseHealthCheck();

    // ...
}
```


## Built With

* [ClickHouse.Net](https://github.com/ilyabreev/ClickHouse.Net)
* [Microsoft.Extensions.Diagnostics.HealthChecks](https://www.nuget.org/packages/Microsoft.AspNetCore.Diagnostics.HealthChecks)

