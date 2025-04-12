namespace CoreBanking.Infrastructure.Data;

public class CoreBankingContextFactory : IDesignTimeDbContextFactory<CoreBankingDbContext>
{
    public CoreBankingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CoreBankingDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=corebanking;Username=postgres;Password=123456");
        return new CoreBankingDbContext(optionsBuilder.Options);
    }
}
