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

//display logo
ConsoleHelper.DisplayMoneyTrackerLogoOther();

//ask for account to load or create new
Account bank = ConsoleHelper.DisplayAccountSelection();

//Start Main MENU loop
while (keepRunning)
{
    //AnsiConsole.Clear();
    ConsoleHelper.RenderTable(bank);
    AnsiConsole.WriteLine();

    var prompt = ConsoleHelper.DisplayMainMenu(options);
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




