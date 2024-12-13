// See https://aka.ms/new-console-template for more information
using System.Text;
using MoneyTracking;
using MoneyTracking.Accounts;
using MoneyTracking.Transactions;
using Spectre.Console;

Console.OutputEncoding = Encoding.Unicode;

Dictionary<string, MENUCHOICES> choices = new Dictionary<string, MENUCHOICES>();
choices.Add("Save and Quit", MENUCHOICES.SAVEANDQUIT);
choices.Add("Add NEW", MENUCHOICES.NEW);
choices.Add("Show Table", MENUCHOICES.SHOW);
choices.Add("Sort Options", MENUCHOICES.SORT);
choices.Add("Edit Mode", MENUCHOICES.EDIT);

var options = choices.Select(x => x.Key)
                   .ToArray();

bool keepRunning = true;

// Initialize account with salary deposit
Account bank = BankAccountBuilder.Empty()
    .WithName("SWEDBANK")
    .Build();

var sizeOfData = bank.LoadFromFile();
if (sizeOfData == -1 || bank.Transactions.Count == 0)
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
else
{
    AnsiConsole.MarkupLine($"[blue]Transactions data loaded:[/] {sizeOfData} [blue]bytes[/]");
}

//DemoData.generate(bank.Transactions, bank);


//JsonSerializerHelper.SerializeToFile(file, bank.Transactions);




while (keepRunning)
{
    //AnsiConsole.Clear();
    AnsiConsole.Write(renderTable(bank));
    AnsiConsole.WriteLine();

    var prompt = displayMenu(options);
    switch (choices.GetValueOrDefault(prompt))
    {
        case MENUCHOICES.SAVEANDQUIT:
            bank.SaveToFile();
            keepRunning = false; break;
        case MENUCHOICES.SHOW:
            break;
        case MENUCHOICES.NEW:
            createNewTransaction(bank);
            break;
    }

}

void createNewTransaction(Account account)
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

    if (AskTransactionConfirmation(t))
    {
        account.Transactions.Add(t);
    }

}

static bool AskTransactionConfirmation(Transaction t)
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

const int RIGHT = 3;
static Table renderTable(Account bank)
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

    foreach (var item in bank.Transactions)
    {
        table.AddRow(item.TransactionDate.ToString("MMM dd"),
            item.Type.ToString(),
            item.Amount.ToString("C"),
            item.Message


            );
    }
    return table;
}

static string displayMenu(string[] choices)
{
    return AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Select and option:")
            .AddChoices(choices));
}