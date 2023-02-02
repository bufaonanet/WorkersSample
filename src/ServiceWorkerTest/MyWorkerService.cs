public class MyWorkerService : BackgroundService
{
    private readonly ILogger<MyWorkerService> _logger;

    public MyWorkerService(ILogger<MyWorkerService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Service Started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}.", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogInformation("Stopping Service!");
    }
}