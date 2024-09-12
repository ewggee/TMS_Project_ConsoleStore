namespace ConsoleStore.Common.Services.PrintService;

public static class PrintService
{
    readonly static string SEPARATOR = new('-', 70);

    /// <inheritdoc/>
    public static void PrintSeparator()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(SEPARATOR);
        Console.ResetColor();
    }

    /// <inheritdoc/>
    public static void PrintColorMessage(string message, ConsoleColor color, bool newLines = true)
    {
        Console.ForegroundColor = color;

        if (newLines)
            Console.WriteLine($"\n{message}\n");
        else
            Console.Write(message);

        Console.ResetColor();
    }
}
