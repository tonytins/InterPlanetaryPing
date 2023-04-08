using InterPlanetaryPing;
using Serilog.Events;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    // .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();

try
{

    // Log that the service is starting up
    Log.Information("Starting up service.");

    // Create a host using the default builder and configure the services
    IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices(services =>
        {
            // Add the Worker as a hosted service
            services.AddHostedService<Worker>();

            // Add Windows service if the operating system is Windows
            if (OperatingSystem.IsWindows())
                services.AddWindowsService(opts =>
                {
                    opts.ServiceName = "InterPlanetary Ping";
                });
        })
        .UseSerilog()
        .Build();

    // Parse command line arguments and configure the service using the options
    Parser.Default.ParseArguments<Config>(args)
        .WithParsed<Config>(opt =>
        {
            // Create a new Config object using the options
            var cfg = new Config
            {
                CID = opt.CID,
                Delay = opt.Delay,

            };

            // Choose the appropriate configuration file based on whether the debugger is attached or not
            var cfgFile = GlobalConsts.CONFIG_FILE;
            if (Debugger.IsAttached)
                cfgFile = GlobalConsts.CONFIG_FILE_DBG;

            // Write the configuration file if it doesn't exist
            if (!File.Exists(cfgFile))
            {
                var toml = Toml.FromModel(cfg);
                File.WriteAllText(cfgFile, toml);
            }

            // Log that the configuration file was written
            Log.Information($"Wrote config file to {cfgFile}");
        });

    // Run the host asynchronously
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