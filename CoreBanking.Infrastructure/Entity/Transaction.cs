namespace CoreBanking.Infrastructure.Entity;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateUtc { get; set; }
    public TransactionType Types { get; set; }
    public Guid AccountId { get; set; }
    [JsonIgnore]
    public Account Account { get; set; } = default!;
}
public enum TransactionType
{
    Deposit,
    Withdraw
}
