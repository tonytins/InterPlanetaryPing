namespace InterPlanetaryPing;

public class Worker : BackgroundService
{
    readonly ILogger<Worker> _logger;
    readonly Options? _options;
    IpfsClient? _client;

    public Worker(ILogger<Worker> logger, Options options)
    {
        _options = options;
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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var delay = TimeSpan.FromMinutes(5);
        var peers = new List<MultiHash>
        {
            new MultiHash("QmY6YauXGkrJtGwFvE3RVukqwBiRaTqgDgY9ru8CY9bR9V"),
            new MultiHash("QmYRmQwU99oLMQ5jL6maXbtTx1DypJk5uXRzURmcCwVe9N")
        };

        if (_client != null)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                foreach (var peer in peers)
                {
                    var stats = await _client.PingAsync(peer, 10, stoppingToken);

                    foreach (var stat in stats)
                    {
                        if (stat.Success)
                            _logger.LogInformation($"{stat.Text}. Status: Success.", DateTimeOffset.Now);
                        else
                            _logger.LogWarning($"{stat.Text}. Status: Failure.", DateTimeOffset.Now);
                    }
                }

                await Task.Delay(delay, stoppingToken);
            }
        }
    }
}

