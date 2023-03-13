namespace Lab2.Task3;

public static class ConsoleHelper
{
    public static int ParseInput(string message)
    {
        Console.Write(message);

        string input = Console.ReadLine()?.Trim() ?? "";
        int.TryParse(input, out int result);

        return result;
    }
}
