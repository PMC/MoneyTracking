namespace MoneyTracking.Accounts;

public class BankAccount : Account
{
    public BankAccount(Guid accountID, string accountName) : base(accountID, accountName)
    {
        AccountType = "Bank";
    }
    public BankAccount(string accountName) : base(accountName)
    {
        AccountType = "Bank";
    }
    //public new string AccountType => "Bank";
}
