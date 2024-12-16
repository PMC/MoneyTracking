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


    public static void RenderTable(Account account)
    {
        Span<Transaction> sortOrder;

        switch (account.SortOrder)
        {
            case Account.SORTORDER.DATE:
                sortOrder = TransactionExtensions.SortByDate(account.Transactions);
                break;
            case Account.SORTORDER.AMOUNT:
                sortOrder = TransactionExtensions.SortByAmount(account.Transactions);
                break;
            case Account.SORTORDER.DEPOSIT:
                sortOrder = TransactionExtensions.FilterByPositiveAmount(account.Transactions);
                break;
            case Account.SORTORDER.WITHDRAW:
                sortOrder = TransactionExtensions.FilterByNegativeAmount(account.Transactions);
                break;
            case Account.SORTORDER.MESSAGE:
                sortOrder = TransactionExtensions.SortByMessage(account.Transactions);
                break;
            default:
                sortOrder = TransactionExtensions.SortByDate(account.Transactions);
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
                DisplayTransactionType(item, account),
                item.Amount.ToString("C"),
                item.Message
                );
        }
        table.Caption(DisplayTableCaption(account, accountSummary));
        table.Title("[bold]Transaction list for:[/][green] " + account.AccountName + "[/]");
        AnsiConsole.Write(table);

    }

    private static string DisplayTableCaption(Account account, decimal accountSummary)
    {
        if (account.AccountType == "Bank")
        {
            if (accountSummary > 0)
            {
                return "[grey]You currently have[/] [green]" + accountSummary.ToString("C") + "[/][grey] on your account[/]";
            }
            else
            {
                return "[grey]You currently have[/] [red]" + accountSummary.ToString("C") + "[/][grey] on your account[/]";
            }
        }
        else
        {
            if (accountSummary > 0)
            {
                return $"[red]You owe {account.AccountName}: " + accountSummary.ToString("C") + "[/]";
            }
            else
            {
                return $"[grey]{account.AccountName} owes you[/]:[green] " + accountSummary.ToString("C") + "[/]";
            }
        }


    }

    private static string DisplayTransactionType(Transaction item, Account account)
    {
        if (account.AccountType == "Bank")
        {
            return item.Type.ToString();
        }

        if (item.Type == TransactionType.Deposit)
        {
            return "Payback";
        }
        else
        {
            return "Loan";
        }
    }

    public static string DisplayMainMenu(string[] choices)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select an option:")
                .WrapAround()
                .AddChoices(choices));
    }
    public static AccountStruct SelectAccountToLoad(AccountStruct[] choices)
    {
        return AnsiConsole.Prompt(
            new SelectionPrompt<AccountStruct>()
                .Title("Select Account to load:")
                .WrapAround()
                .AddChoices(choices)
                .AddChoices(new AccountStruct
                {
                    AccountID = Guid.NewGuid(),
                    AccountName = "*NEW ACCOUNT*",
                })
                );
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
            if (account.AccountType == "Bank")
            {
                message = AnsiConsole.Prompt(new TextPrompt<string>("Payment to?: ")
                    .DefaultValueStyle(Color.Blue)
                    .DefaultValue($"BankOMat utag: {account.AccountName}"));
                amount = 200M;
            }
            else
            {
                message = AnsiConsole.Prompt(new TextPrompt<string>("Message: ")
                    .DefaultValueStyle(Color.Blue)
                    .DefaultValue($"Loan"));
                amount = 200M;
            }
        }
        else
        {
            if (account.AccountType == "Bank")
            {

                message = AnsiConsole.Prompt(new TextPrompt<string>("Deposit from who?: ")
                    .DefaultValueStyle(Color.Blue)
                    .DefaultValue("Salary"));
                amount = 12000M;
            }
            else
            {
                message = AnsiConsole.Prompt(new TextPrompt<string>("Message: ")
                    .DefaultValueStyle(Color.Blue)
                    .DefaultValue("Payback"));
                amount = 500;

            }
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

    internal static string AskForAccountType()
    {
        AnsiConsole.MarkupLine("Bank Account is for displaying transactions against a Bank");
        AnsiConsole.MarkupLine("Person Account is for keeping track of Loans and Paybacks towards one person\n");
        return AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select Account Type:")
                .WrapAround()
                .AddChoices("Person")
                .AddChoices("Bank")
                );
    }

    internal static string AskForAccountName(string defaultName)
    {
        return AnsiConsole.Prompt(new TextPrompt<string>("Please enter an Account name: ")
            .DefaultValueStyle(Color.Blue)
            .DefaultValue(defaultName));
    }

    public static Account DisplayAccountSelection()
    {
        AccountList accountList = new AccountList();
        Account bank = null;
        AccountStruct? selection = null;

        // loading Account data
        AnsiConsole.Markup($"[blue]Loading Account data... [/]");
        int accountListSize = accountList.load();

        if (accountListSize != -1)
        {
            AnsiConsole.MarkupLine($"{accountListSize} [blue]bytes loaded[/]");
            selection = ConsoleHelper.SelectAccountToLoad(accountList.Accounts.ToArray());
        }
        else
        {
            AnsiConsole.MarkupLine("[white]No data found![/]");
        }

        if (accountListSize == -1 || selection?.AccountName == "*NEW ACCOUNT*")
        {
            string type = ConsoleHelper.AskForAccountType();
            string name;
            if (type == "Bank")
            {
                name = ConsoleHelper.AskForAccountName("SWEDBANK");
                bank = BankAccountBuilder.Empty()
                    .WithName(name)
                    .Build();
            }
            else
            {
                name = ConsoleHelper.AskForAccountName("John Doe");
                bank = PersonAccountBuilder.Empty()
                    .WithName(name)
                    .Build();
            }
            accountList.AddNew(bank);
        }
        else
        {
            if (selection.AccountType == "Bank")
            {
                bank = BankAccountBuilder.Empty()
                    .WithName(selection.AccountName)
                    .WithAccountID(selection.AccountID)
                    .Build();
            }
            else
            {
                bank = PersonAccountBuilder.Empty()
                    .WithName(selection.AccountName)
                    .WithAccountID(selection.AccountID)
                    .Build();
            }
        }

        accountList.save();

        //load transactions
        var sizeOfData = bank.LoadFromFile();
        if (sizeOfData == -1 || bank.Transactions.Count == 0)
        {
            ConsoleHelper.AskIfUserWantDemoData(bank);
        }
        else
        {
            AnsiConsole.MarkupLine($"[blue]Loading Transaction data... [/]{sizeOfData} [blue]bytes loaded[/]\n");
        }

        return bank;
    }
}
