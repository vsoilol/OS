using System.Text;

object syncToken = new object();

Console.OutputEncoding = Encoding.UTF8;

int[] array = CreateArray();
Console.WriteLine("Исходный массив:");
DisplayArray(array);

Thread sortingThread = new Thread(new ParameterizedThreadStart(SortingThreadMain!));
Thread displayingThread = new Thread(new ParameterizedThreadStart(DisplayingThreadMain!));

sortingThread.Start(array);
Thread.Sleep(200);
displayingThread.Start(array);

void SortingThreadMain(object data)
{
    int[]? array = data as int[];

    if (array == null)
    {
        return;
    }

    // Вход в критическую секцию
    lock (syncToken)
    {
        Console.WriteLine("Сортировка....");
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                int tmp;
                if (array[j] > array[j + 1])
                {
                    tmp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = tmp;
                    Thread.Sleep(200);
                }
            }
        }
    }
}

void DisplayingThreadMain(object data)
{
    int[]? array = data as int[];
    if (array == null)
    {
        return;
    }

    // Вход в критическую секцию
    lock (syncToken)
    {
        DisplayArray(array);
        Console.ReadKey();
    }
}

int[] CreateArray()
{
    Console.Write("Введите размерность массива: ");
    string arraySizeString = Console.ReadLine();
    int arraySize;
    if (!int.TryParse(arraySizeString, out arraySize))
    {
        arraySize = 0;
    }
    int[] array = new int[arraySize];
    Random rand = new Random();
    for (int i = 0; i < arraySize; i++)
    {
        array[i] = -1 * rand.Next(99) + rand.Next(99);
    }
    return array;
}

void DisplayArray(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        Console.Write(array[i] + " ");
    }
    Console.WriteLine();
}
