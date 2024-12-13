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

    public static void AskMultiSelection(Account account)
    {
        var toDelete = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Transaction>()
                    .PageSize(Console.WindowHeight - 3)
                    .WrapAround()
                    .NotRequired()
                    .Title("[blue]MultiSelect Transactions to REMOVE[/]")
                    .MoreChoicesText("[grey](Move up and down to reveal more...[/]")
                    .InstructionsText("[grey](Press [blue]<space>[/] to toggle a Transaction, [green]<enter>[/] to accept)[/]")

                    .AddChoices(account.Transactions));

        foreach (var t in toDelete)
        {
            account.Transactions.Remove(t);
            AnsiConsole.MarkupLine("[red]Removing Record with TransactionID: [/]" + t.TransactionId.ToString());
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


    public static void RenderTable(Account bank)
    {
        Span<Transaction> sortOrder;

        switch (bank.SortOrder)
        {
            case Account.SORTORDER.DATE:
                sortOrder = TransactionExtensions.SortByDate(bank.Transactions);
                break;
            case Account.SORTORDER.AMOUNT:
                sortOrder = TransactionExtensions.SortByAmount(bank.Transactions);
                break;
            case Account.SORTORDER.DEPOSIT:
                sortOrder = TransactionExtensions.FilterByPositiveAmount(bank.Transactions);
                break;
            case Account.SORTORDER.WITHDRAW:
                sortOrder = TransactionExtensions.FilterByNegativeAmount(bank.Transactions);
                break;
            case Account.SORTORDER.MESSAGE:
                sortOrder = TransactionExtensions.SortByMessage(bank.Transactions);
                break;
            default:
                sortOrder = TransactionExtensions.SortByDate(bank.Transactions);
                break;

        }

        // Create a table
        Table table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Blue)
            .AddColumn(new TableColumn("[grey]Date[/]").PadRight(RIGHT))
            .AddColumn(new TableColumn("[grey]Type[/]").PadRight(RIGHT))
            .AddColumn(new TableColumn("[grey]Amount[/]").RightAligned().PadLeft(RIGHT))
            .AddColumn(new TableColumn("[grey]Transaction Message[/]").PadRight(RIGHT))
            ;
        decimal accountSummary = 0M;
        foreach (var item in sortOrder)
        {
            accountSummary += item.Amount;
            table.AddRow(item.TransactionDate.ToShortDateString(),
                item.Type.ToString(),
                item.Amount.ToString("C"),
                item.Message
                );
        }
        if (accountSummary > 0)
        {
            table.Caption("[grey]You currently have[/] [green]" + accountSummary.ToString("C") + "[/][grey] on your account[/]");
        }
        else
        {
            table.Caption("[grey]You currently have[/] [red]" + accountSummary.ToString("C") + "[/][grey] on your account[/]");

        }

        AnsiConsole.Write(table);

    }

    public static string DisplayMenu(string[] choices)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select and option:")
                .WrapAround()
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

    public static void AskSortingOrder(Account bank)
    {
        Dictionary<string, Account.SORTORDER> choices = new Dictionary<string, Account.SORTORDER>();
        choices.Add("Transaction Date", Account.SORTORDER.DATE);
        choices.Add("Transaction Message", Account.SORTORDER.MESSAGE);
        choices.Add("Amount", Account.SORTORDER.AMOUNT);
        choices.Add("Only Deposit", Account.SORTORDER.DEPOSIT);
        choices.Add("Only Withdraw", Account.SORTORDER.WITHDRAW);


        var sortOrder = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
        .Title("[green]Select sorting[/] [blue]ORDER[/]")
        .PageSize(10)
        .WrapAround()
        .MoreChoicesText("[grey](Move up and down to reveal more)[/]")
        .AddChoices(choices.Select(x => x.Key)
                   .ToArray()
        ));

        bank.SortOrder = choices.GetValueOrDefault(sortOrder);
    }


}
