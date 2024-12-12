namespace MoneyTracking.Accounts;

public class PersonAccount : Account
{
    public PersonAccount(Guid accountID, string accountName) : base(accountID, accountName)
    {
    }
    public PersonAccount(string accountName) : base(accountName)
    {
    }
    public new string AccountType => "Person";

}
