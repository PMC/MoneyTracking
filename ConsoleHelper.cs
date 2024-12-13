using MoneyTracking.Accounts;
using MoneyTracking.Transactions;
using Spectre.Console;

namespace MoneyTracking;
public class ConsoleHelper
{
    const int RIGHT = 3;
    public static void DisplayMoneyTrackerLogo()
    {
        AnsiConsole.Write(
        new FigletText("Money Tracker")
            .LeftJustified()
            .Color(Color.DeepSkyBlue3_1));

    }

    public static void DisplayMoneyTrackerLogoOther()
    {
        var font = FigletFont.Load(Path.Combine("Figlets", "cricket.flf"));

        AnsiConsole.Write(
        new FigletText(font, "MONEY TRACKER")
            .LeftJustified()
            .Color(Color.DeepSkyBlue3_1));
    }

    public static void AskMultiSelection(Span<Transaction> sortedSpan)
    {
        var favorites = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Transaction>()
                    .PageSize(10)
                    .Title("[blue]MultiSelect Transactions to delete[/]")
                    .MoreChoicesText("[grey](Move up and down to reveal more...[/]")
                    .InstructionsText("[grey](Press [blue]<space>[/] to toggle a Transaction, [green]<enter>[/] to accept)[/]")

                    .AddChoices(sortedSpan.ToArray()));

        foreach (var t in favorites)
        {
            AnsiConsole.WriteLine(t.TransactionId.ToString());
        }
    }

    public static void AskIfUserWantDemoData(Account bank)
    {
        var confirmation = AnsiConsole.Prompt(
        new TextPrompt<bool>("[green]No Transactions found, do you want to generate demo data ?[/]")
            .AddChoice(true)
            .AddChoice(false)
            .DefaultValue(true)
            .DefaultValueStyle(Color.Blue)
            .WithConverter(choice => choice ? "Y" : "n"));

        if (confirmation)
        {
            DemoData.generate(bank.Transactions, bank);
        }
    }


    public static bool AskTransactionConfirmation(Transaction t)
    {
        AnsiConsole.Write(new Table()
            .Border(TableBorder.MinimalHeavyHead)
            .BorderColor(Color.Blue)
            .AddColumn(new TableColumn("Key"))
            .AddColumn(new TableColumn("Value"))
                    .AddRow(new Markup("TransactionID"), new Markup($"[green]{t.TransactionId}[/]"))
                    .AddRow(new Markup("Transaction Date"), new Markup($"[green]{t.TransactionDate.ToShortDateString()}[/]"))
                    .AddRow(new Markup("Transaction Message"), new Markup($"[green]{t.Message}[/]"))
                    .AddRow(new Markup("Transaction Type"), new Markup($"[green]{t.Type.ToString()}[/]"))
                    .AddRow(new Markup("Transaction Amount"), new Markup($"[green]{t.Amount.ToString("C")}[/]"))
                    .AddRow(new Markup("AccountID"), new Markup($"[green]{t.AccountID.ToString()}[/]"))

                    );
        AnsiConsole.WriteLine();

        return AnsiConsole.Prompt(
            new SelectionPrompt<bool> { Converter = value => value ? "Yes" : "No" }
            .Title("[green]Proceed with commiting the Transaction?[/]: ")
            .AddChoices(true, false));
    }


    public static Table RenderTable(Account bank)
    {
        // Create a table
        Table table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .Caption($"Transaction History for {bank.AccountName}")
            .AddColumn(new TableColumn("[grey]Date[/]").PadRight(RIGHT))
            .AddColumn(new TableColumn("[grey]Type[/]").PadRight(RIGHT))
            .AddColumn(new TableColumn("[grey]Amount[/]").RightAligned().PadLeft(RIGHT))
            .AddColumn(new TableColumn("[grey]Transaction Message[/]").PadRight(RIGHT))
            ;

        foreach (var item in TransactionExtensions.SortByDate(bank.Transactions))
        {
            table.AddRow(item.TransactionDate.ToString("MMM dd"),
                item.Type.ToString(),
                item.Amount.ToString("C"),
                item.Message


                );
        }
        return table;
    }

    public static string DisplayMenu(string[] choices)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select and option:")
                .AddChoices(choices));
    }

    public static void CreateNewTransaction(Account account)
    {
        string message;
        decimal amount;
        var transactionDate = AnsiConsole.Prompt(new TextPrompt<DateTime>("Enter Transaction Date: ")
            .DefaultValueStyle(Color.Blue)
            .DefaultValue(DateTime.Today));
        var type = AnsiConsole.Prompt(
        new SelectionPrompt<TransactionType>()
            .Title("Select TransactionType: ")
            .AddChoices(TransactionType.Deposit, TransactionType.Withdraw
            ));

        if (type == TransactionType.Withdraw)
        {
            message = AnsiConsole.Prompt(new TextPrompt<string>("Payment to?: ")
                .DefaultValueStyle(Color.Blue)
                .DefaultValue($"BankOMat utag: {account.AccountName}"));
            amount = 200M;
        }
        else
        {
            message = AnsiConsole.Prompt(new TextPrompt<string>("Deposit from who?: ")
                .DefaultValueStyle(Color.Blue)
                .DefaultValue("Salary"));
            amount = 12000M;
        }

        amount = AnsiConsole.Prompt(new TextPrompt<decimal>("Amount?: ")
            .DefaultValueStyle(Color.Blue)
            .DefaultValue(amount));

        Transaction t = TransactionBuilder.Empty()
            .WithAccountID(account.AccountID)
            .WithMessage(message)
            .WithAmount(amount)
            .WithTransactionType(type)
            .WithTransactionDate(transactionDate)
            .Build();

        if (ConsoleHelper.AskTransactionConfirmation(t))
        {
            account.Transactions.Add(t);
        }

    }
}
