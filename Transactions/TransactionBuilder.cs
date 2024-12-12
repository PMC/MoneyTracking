using MoneyTracking.Accounts;

namespace MoneyTracking.Transactions;

public class TransactionBuilder
{
    private Guid _accountID;
    private decimal _amount;
    private DateTime _transactionDate;
    private string _message;
    private TransactionType _type; // withdraw or deposit

    public static TransactionBuilder Empty()
    {
        return new TransactionBuilder();
    }

    public TransactionBuilder WithTransactionType(TransactionType type)
    {
        _type = type;
        return this;
    }
    public TransactionBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public TransactionBuilder WithMessage(string message)
    {
        _message = message;
        return this;
    }

    public TransactionBuilder WithAccountID(Guid accountID)
    {
        _accountID = accountID;
        return this;
    }
    public TransactionBuilder WithAccountID(Account account)
    {
        _accountID = account.AccountID;
        return this;
    }
    public TransactionBuilder WithNewAccountID()
    {
        _accountID = Guid.NewGuid();
        return this;
    }
    public TransactionBuilder WithTransactionDate(DateTime date)
    {
        _transactionDate = date;
        return this;
    }
    public Transaction Build()
    {
        if (_transactionDate == default)
        {
            _transactionDate = DateTime.Today;
        }
        if (_type == TransactionType.Withdraw && _amount >= 0)
        {
            _amount = 0 - _amount;
        }
        return new Transaction
        {
            TransactionId = Guid.NewGuid(),
            AccountID = _accountID,
            Amount = _amount,
            Message = _message,
            TransactionDate = _transactionDate,
            Type = _type
        };
    }
    private TransactionBuilder() { }

}
