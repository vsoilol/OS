namespace Lab3.Task1;

public static class ConsoleHelper
{
    public static int GetValueFromInput(string message)
    {
        Console.Write(message);

        string arraySizeString = Console.ReadLine() ?? "";
        int data;

        if (!int.TryParse(arraySizeString, out data))
        {
            data = 0;
        }

        return data;
    }
}
