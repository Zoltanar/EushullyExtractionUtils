using System;
using System.Linq;

namespace EushullyExtractionUtils;

public static class ConsoleUtils
{
    public const ConsoleColor ErrorColor = ConsoleColor.Red;
    public const ConsoleColor WarningColor = ConsoleColor.Yellow;
    public const ConsoleColor SuccessColor = ConsoleColor.Green;

    private static bool RewriteWithNextLine { get; set; }

    public static void Print(ConsoleColor color, string message, bool rewriteWithNextLine = false)
    {
        if (RewriteWithNextLine)
        {
            //go back to left and write blank line
            Console.CursorLeft = 0;
            Console.Write(new string(Enumerable.Repeat(' ', Console.WindowWidth - 1).ToArray()));
            //go back to left to overwrite blank line with new line
            Console.CursorLeft = 0;
        }

        RewriteWithNextLine = rewriteWithNextLine;
        Console.ForegroundColor = color;
        Console.Write(message);
        if (!rewriteWithNextLine) Console.WriteLine();
        Console.ResetColor();
    }


    public static void PrintError(string text)
    {
        Print(ErrorColor, text.TrimEnd('\r', '\n'));
    }

    public static void PrintWarning(string text)
    {
        Print(WarningColor, text.TrimEnd('\r', '\n'));
    }

    public static void Print(string text)
    {
        Print(SuccessColor, text.TrimEnd('\r', '\n'));
    }

}