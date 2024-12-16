namespace MoneyTracking.Accounts;
using MoneyTracking.Transactions;
public abstract class Account
{
    public Guid AccountID { get; protected set; }
    public decimal Balance { get; protected set; } = 0;
    public string AccountName { get; protected set; }
    public string TransactionFile { get; protected set; }
    public string AccountType { get; protected set; }
    public List<Transaction> Transactions { get; set; } = [];

    public enum SORTORDER { DATE, AMOUNT, DEPOSIT, MESSAGE, WITHDRAW }

    public SORTORDER SortOrder { get; set; }

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
        TransactionFile = accountID.GetHashCode().ToString() + ".json";
    }
    protected Account(string accountName)
    {
        AccountID = Guid.NewGuid();
        AccountName = accountName;
        TransactionFile = AccountID.GetHashCode().ToString() + ".json";
    }

    public void SaveToFile()
    {
        //var file = JsonSerializerHelper.GetFullPathToJsonFile($"{AccountID.GetHashCode()}.json");
        var file = JsonSerializerHelper.GetFullPathToJsonFile(TransactionFile);
        JsonSerializerHelper.SerializeToFile(file, Transactions);
    }

    /**
     * Load file and return datasize or -1 if json file dont exists
     */
    public int LoadFromFile()
    {
        var file = JsonSerializerHelper.GetFullPathToJsonFile(TransactionFile);

        if (File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            Transactions = JsonSerializerHelper.Deserialize<List<Transaction>>(jsonData);
            return jsonData.Length;
        }

        return -1;
    }
}
