namespace MoneyTracking.Accounts;
using MoneyTracking.Transactions;
public abstract class Account
{
    public Guid AccountID { get; protected set; }
    public decimal Balance { get; protected set; } = 0;
    public string AccountName { get; protected set; }
    public string? TransactionFile { get; protected set; }
    public string? AccountType { get; protected set; }
    public List<Transaction> Transactions { get; protected set; } = [];

    public virtual void Deposit(decimal amount)
    {
        if (amount > 0)
            Balance += amount;
    }
    public virtual bool Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > Balance)
            return false;

        Balance -= amount;
        return true;
    }

    protected Account(Guid accountID, string accountName)
    {
        AccountID = accountID;
        AccountName = accountName;
        TransactionFile = accountName;
    }
    protected Account(string accountName)
    {
        AccountID = Guid.NewGuid();
        AccountName = accountName;
        TransactionFile = accountName;
    }
}
