namespace MoneyTracking.Transactions;
public class Transaction
{
    public decimal Amount { get; set; }
    public Guid AccountID { get; set; }
    public Guid TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Message { get; set; }
    public TransactionType Type { get; set; } // withdraw or deposit
}

public enum TransactionType
{
    Withdraw,
    Deposit
}
