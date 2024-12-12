using MoneyTracking.Accounts;
using MoneyTracking.Transactions;

namespace MoneyTracking;
public class DemoData
{
    public static void generate(List<Transaction> transactions, Account bank)
    {
        transactions.Add(TransactionBuilder.Empty()
            .WithAccountID(bank.AccountID)
            .WithMessage("Initial Salary")
            .WithAmount(16000)
            .WithTransactionDate(DateTime.Today.AddDays(-60))
            .WithTransactionType(TransactionType.Deposit)
        .Build());

        // Simulate withdrawals using range
        int withdrawalCount = 3;
        int days = -59;
        decimal withdrawalAmount = 150;
        var withdrawals = Enumerable.Range(1, withdrawalCount).Select(i =>
             TransactionBuilder.Empty()
                .WithAccountID(bank.AccountID)
                .WithMessage($"BankOMat: Withdrawal {i}")
                .WithAmount(withdrawalAmount)
                .WithTransactionType(TransactionType.Withdraw)
                .WithTransactionDate(DateTime.Today.AddDays(days++))
            .Build()
        );

        transactions.AddRange(withdrawals);

        // Simulate withdraw using range
        int depositCount = 7;
        decimal[] depositAmounts = { 200, 300, 450, 600, 7000, 50.6M, 75 };
        string[] onlineSore = { "Webhallen", "Dustin Home", "Spotify", "Comviq", "Hyra", "Spectra", "Lexicon" };
        var deposits = Enumerable.Range(1, depositCount).Select(i =>
               TransactionBuilder.Empty()
               .WithAccountID(bank.AccountID)
               .WithMessage($"{onlineSore[i - 1]}")
               .WithAmount(depositAmounts[i - 1])
               .WithTransactionType(TransactionType.Withdraw)
               .WithTransactionDate(DateTime.Today.AddDays(days++))
               .Build()
        );

        transactions.AddRange(deposits);
    }
}
