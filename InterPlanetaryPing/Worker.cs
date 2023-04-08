namespace InterPlanetaryPing;

public class Worker : BackgroundService
{
    readonly ILogger<Worker> _logger;
    readonly Config? _config;
    IpfsClient? _client;

    public Worker(ILogger<Worker> logger, Config config)
    {
        _config = config;
        _logger = logger;
    }

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {

        _client = new IpfsClient();
        return base.StartAsync(cancellationToken);
    }

    /// <summary>
    /// Executes the worker's tasks asynchronously.
    /// </summary>
    /// <param name="stoppingToken">The cancellation token.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Choose the appropriate configuration file based on whether the debugger is attached or not
        var cfgFile = GlobalConsts.CONFIG_FILE;
        if (Debugger.IsAttached)
            cfgFile = GlobalConsts.CONFIG_FILE_DBG;

        // Check if the client exists and read the configuration file
        if (_client != null)
        {
            if (File.Exists(GlobalConsts.CONFIG_FILE))
            {
                var content = File.ReadAllText(GlobalConsts.CONFIG_FILE);
                var config = Toml.ToModel<Config>(content);

                // Perform the worker's tasks in a loop until cancellation is requested
                while (!stoppingToken.IsCancellationRequested)
                {
                    // Ping the specified content identifier and log the results
                    var stats = await _client.PingAsync(new MultiHash(config.CID), 10, stoppingToken);

                    foreach (var stat in stats)
                    {
                        if (stat.Success)
                            _logger.LogInformation(stat.Text, DateTimeOffset.Now);
                        else
                            _logger.LogWarning(stat.Text, DateTimeOffset.Now);
                    }

                    // Delay for the specified amount of time before performing the next ping
                    await Task.Delay(TimeSpan.FromMinutes(config.Delay), stoppingToken);
                }
            }
        }
        else
        {
            // Log a critical message if the client is not found
            _logger.LogCritical("Client not found.", DateTimeOffset.Now);
        }
    }
}

