using System.Text;
using Lab2.Task2;

object syncToken = new object();

Console.OutputEncoding = Encoding.UTF8;

var arraySize = ConsoleHelper.GetValueFromInput("Введите размерность массива: ");
var array = ArrayHelper.CreateArray(arraySize);

Console.WriteLine("Исходный массив:");
Console.WriteLine(ArrayHelper.ToString(array));

Thread worker1 = new Thread(new ParameterizedThreadStart(WorkerOneTask!));
Thread worker2 = new Thread(new ParameterizedThreadStart(WorkerTwoTask!));

worker1.Start(array);
Thread.Sleep(100);
worker2.Start(array);

worker1.Join();
worker2.Join();

Console.WriteLine("Program is finish");

void WorkerOneTask(object data)
{
    int[]? array = data as int[];

    if (array == null)
    {
        return;
    }

    lock (syncToken)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] % 2 != 0)
            {
                array[i] = -5;
            }
        }

        Console.WriteLine("Обработанный массив:");
        Console.WriteLine(ArrayHelper.ToString(array));
    }
}

void WorkerTwoTask(object data)
{
    int[]? array = data as int[];

    if (array == null)
    {
        return;
    }

    lock (syncToken)
    {
        int minIndex = 0;
        int maxIndex = 0;

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < array[minIndex])
            {
                minIndex = i;
            }
            else if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        Console.WriteLine($"Индекс минимального значения {array[minIndex]} = {minIndex}");
        Console.WriteLine($"Индекс максимального значения {array[maxIndex]} = {maxIndex}");
    }
}
