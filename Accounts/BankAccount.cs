namespace MoneyTracking.Accounts;

public class BankAccount : Account
{
    public BankAccount(Guid accountID, string accountName) : base(accountID, accountName)
    {
    }
    public BankAccount(string accountName) : base(accountName)
    {
    }
    public new string AccountType => "Bank";
}
