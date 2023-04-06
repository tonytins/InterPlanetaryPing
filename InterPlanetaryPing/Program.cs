using InterPlanetaryPing;
using Microsoft.Extensions.Configuration;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    // .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();

try
{
    Log.Information("Starting up service.");
    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

    await host.RunAsync();

}
catch (Exception ex)
{
    Log.Fatal(ex, "There was an error when starting the service.");
}
finally
{
    Log.CloseAndFlush();
}