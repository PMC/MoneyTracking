using Spectre.Console;

namespace MoneyTracking;
public class ConsoleHelper
{
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
}
