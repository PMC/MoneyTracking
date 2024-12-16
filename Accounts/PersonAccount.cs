namespace MoneyTracking.Accounts;

public class PersonAccount : Account
{
    public PersonAccount(Guid accountID, string accountName) : base(accountID, accountName)
    {
        AccountType = "Person";
    }
    public PersonAccount(string accountName) : base(accountName)
    {
        AccountType = "Person";
    }
    //public new string AccountType => "Person";

}
