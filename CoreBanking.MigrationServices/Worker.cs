using CoreBanking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.MigrationServices;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _hostApplication;
    private readonly IServiceProvider _serviceProvider;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime hostApplication, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _hostApplication = hostApplication;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("Database not created~");
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CoreBankingDbContext>();
            _logger.LogInformation("Database created~");
            await RunMigrationAsync(dbContext, stoppingToken);
            await SeedDataAsync(dbContext, stoppingToken);
            _logger.LogInformation("Database migration and seeding completed successfully.");
        }
        catch (Exception)
        {
            _logger.LogError("An error occurred during migration or seeding.");
            throw;
        }
        _hostApplication.StopApplication();
    }
    private static async Task RunMigrationAsync(CoreBankingDbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await dbContext.Database.MigrateAsync(cancellationToken);
        });
    }

    private static async Task SeedDataAsync(CoreBankingDbContext dbContext, CancellationToken cancellationToken)
    {


        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
