namespace CoreBanking.MigrationServices;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _hostApplication;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostApplication)
    {
        _logger = logger;
        _hostApplication = hostApplication;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _hostApplication.StopApplication();
        return Task.CompletedTask;
    }
}
