using MoneyTracking.Transactions;

namespace MoneyTracking.Accounts;
public class AccountList
{
    public List<AccountStruct> Accounts { get; set; } = new List<AccountStruct>();
    public void AddNew(Account account)
    {
        var st = new AccountStruct();
        st.AccountID = account.AccountID;
        st.AccountName = account.AccountName;
        st.AccountType = account.AccountType;
        st.TransactionFile = account.AccountID.GetHashCode() + ".json";
        Accounts.Add(st);
    }

    public void save()
    {
        var file = JsonSerializerHelper.GetFullPathToJsonFile("accountList.json");
        JsonSerializerHelper.SerializeToFile(file, Accounts);
    }

    public int load()
    {
        var file = JsonSerializerHelper.GetFullPathToJsonFile("accountList.json");
        if (File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            Accounts = JsonSerializerHelper.Deserialize<List<AccountStruct>>(jsonData);
            return jsonData.Length;
        }

        return -1;
    }


}
public class AccountStruct
{
    public Guid AccountID { get; set; }
    public string AccountName { get; set; }
    public string TransactionFile { get; set; }
    public string AccountType { get; set; }

    public override string? ToString()
    {
        return AccountType + ": " + AccountName;
    }
}