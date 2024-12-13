namespace MoneyTracking.Transactions;
public class Transaction : IComparable
{
    public decimal Amount { get; set; }
    public Guid AccountID { get; set; }
    public Guid TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Message { get; set; }
    public TransactionType Type { get; set; } // withdraw or deposit

    public int CompareTo(object? obj)
    {
        Transaction t = obj as Transaction;
        return TransactionDate.CompareTo(t.TransactionDate);
    }
}
public enum TransactionType
{
    Withdraw,
    Deposit
}