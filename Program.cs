// See https://aka.ms/new-console-template for more information
using System.Text;
using MoneyTracking;
using MoneyTracking.Accounts;
using Spectre.Console;

Console.OutputEncoding = Encoding.Unicode;

Dictionary<string, MENUCHOICES> choices = new Dictionary<string, MENUCHOICES>();
choices.Add("Add NEW", MENUCHOICES.NEW);
choices.Add("Delete Records", MENUCHOICES.EDIT);
choices.Add("Sort Options", MENUCHOICES.SORT);
choices.Add("Save and Quit", MENUCHOICES.SAVEANDQUIT);

var options = choices.Select(x => x.Key).ToArray();

bool keepRunning = true;


ConsoleHelper.DisplayMoneyTrackerLogoOther(); //display logo


// Initialize account with salary deposit
Account bank = BankAccountBuilder.Empty()
    .WithName("SWEDBANK")
    .Build();

var sizeOfData = bank.LoadFromFile();
if (sizeOfData == -1 || bank.Transactions.Count == 0)
{
    ConsoleHelper.AskIfUserWantDemoData(bank);
}
else
{
    AnsiConsole.MarkupLine($"[blue]Transactions data loaded:[/] {sizeOfData} [blue]bytes[/]\n");
}

AnsiConsole.Markup("Press enter to continue....");
Console.ReadLine();

while (keepRunning)
{
    //AnsiConsole.Clear();
    ConsoleHelper.RenderTable(bank);
    AnsiConsole.WriteLine();

    var prompt = ConsoleHelper.DisplayMenu(options);
    switch (choices.GetValueOrDefault(prompt))
    {
        case MENUCHOICES.SAVEANDQUIT:
            bank.SaveToFile();
            keepRunning = false; break;
        case MENUCHOICES.EDIT:
            ConsoleHelper.AskMultiSelection(bank);
            break;
        case MENUCHOICES.NEW:
            ConsoleHelper.CreateNewTransaction(bank);
            break;
        case MENUCHOICES.SORT:
            ConsoleHelper.AskSortingOrder(bank);
            break;

    }

}




