namespace MoneyTracking.Transactions;

public static class TransactionExtensions
{
    public static Span<Transaction> SortByDate(List<Transaction> transactions)
    {
        return transactions.ToArray().OrderBy(t => t.TransactionDate).ToArray().AsSpan();
    }

    public static Span<Transaction> SortByAmount(List<Transaction> transactions)
    {
        return transactions.ToArray().OrderBy(t => t.Amount).ToArray().AsSpan();
    }

    public static Span<Transaction> FilterByPositiveAmount(List<Transaction> transactions)
    {
        return transactions.ToArray().Where(t => t.Amount >= 0).ToArray().AsSpan();
    }
    public static Span<Transaction> FilterByNegativeAmount(List<Transaction> transactions)
    {
        return transactions.ToArray().Where(t => t.Amount < 0).ToArray().AsSpan();
    }
    public static Span<Transaction> SortByMessage(List<Transaction> transactions)
    {
        return transactions.ToArray().OrderBy(t => t.Message).ToArray().AsSpan();
    }

}
