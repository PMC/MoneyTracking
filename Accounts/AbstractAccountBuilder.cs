namespace MoneyTracking.Accounts;

public abstract class AbstractAccountBuilder<Account>
{
    protected string? _accountName;
    protected string? _transactionFile;

    protected Guid _accountId = Guid.NewGuid();

    public AbstractAccountBuilder<Account> WithName(string accountName)
    {
        _transactionFile = accountName;
        _accountName = accountName;
        return this;
    }
    public AbstractAccountBuilder<Account> WithAccountID(Guid accountID)
    {
        _accountId = accountID;
        return this;
    }

    public abstract Account Build();

}

public class BankAccountBuilder : AbstractAccountBuilder<BankAccount>
{
    public override BankAccount Build()
    {
        return new BankAccount(_accountId, _accountName);
    }

    public static AbstractAccountBuilder<BankAccount> Empty()
    {
        return new BankAccountBuilder();
    }
}

public class PersonAccountBuilder : AbstractAccountBuilder<PersonAccount>
{
    public override PersonAccount Build()
    {
        return new PersonAccount(_accountId, _accountName);
    }

    public static AbstractAccountBuilder<PersonAccount> Empty()
    {
        return new PersonAccountBuilder();
    }
}