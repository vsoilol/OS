using System.Text;
using Lab2.Task3;

object syncToken = new object();

Console.OutputEncoding = Encoding.UTF8;

var arraySize = ConsoleHelper.ParseInput("Введите размерность массива: ");
var arrayOne = ArrayManipulator.GenerateRandomIntArray(arraySize);
var arrayTwo = ArrayManipulator.GenerateRandomIntArray(arraySize);

Console.WriteLine("\nПервый массив:");
Console.WriteLine(ArrayManipulator.ArrayToString(arrayOne));

Console.WriteLine("\nВторой массив:");
Console.WriteLine(ArrayManipulator.ArrayToString(arrayTwo));

Thread worker1 = new Thread(() => WorkerOneTask(new object[] { arrayOne, arrayTwo }));
Thread worker2 = new Thread(() => WorkerTwoTask(new object[] { arrayOne, arrayTwo }));

worker1.Start();
worker2.Start();

worker1.Join();
worker2.Join();

Console.WriteLine("\nProgram is finish");

void WorkerOneTask(object[] args)
{
    if (!(args[0] is int[] arrayOne) || !(args[1] is int[] arrayTwo))
    {
        return;
    }

    lock (syncToken)
    {
        var sumOfPositiveElementsArrayOne = ArrayManipulator.GetSumOfPositiveElements(arrayOne);
        var sumOfPositiveElementsArrayTwo = ArrayManipulator.GetSumOfPositiveElements(arrayTwo);

        Console.WriteLine(
            $"\nСумма положительных элементов первого массива = {sumOfPositiveElementsArrayOne}"
        );

        Console.WriteLine(
            $"Сумма положительных элементов второго массива = {sumOfPositiveElementsArrayTwo}"
        );

        string resultText =
            sumOfPositiveElementsArrayOne > sumOfPositiveElementsArrayTwo
                ? "Сумма положительных элементов первого массива больше чем второго."
                : sumOfPositiveElementsArrayOne == sumOfPositiveElementsArrayTwo
                    ? "Сумма положительных элементов первого массива и второго равны."
                    : "Сумма положительных элементов второго массива больше чем первого.";

        Console.WriteLine(resultText);
    }
}

void WorkerTwoTask(object[] args)
{
    if (!(args[0] is int[] arrayOne) || !(args[1] is int[] arrayTwo))
    {
        return;
    }

    lock (syncToken)
    {
        var countOfPositiveElementsArrayOne = ArrayManipulator.CountPositiveElements(arrayOne);
        var countOfNegativeElementsArrayTwo = ArrayManipulator.CountNegativeElements(arrayTwo);

        Console.WriteLine(
            $"\nКоличество положительных элементов первого массива = {countOfPositiveElementsArrayOne}"
        );

        Console.WriteLine(
            $"Количество отрицательных элементов второго массива = {countOfNegativeElementsArrayTwo}"
        );

        string resultText =
            countOfPositiveElementsArrayOne > countOfNegativeElementsArrayTwo
                ? "Количество положительных элементов в первом массиве больше чем количество отрицательных элементов во втором массиве."
                : countOfPositiveElementsArrayOne == countOfNegativeElementsArrayTwo
                    ? "Количество положительных элементов в первом массиве и количество отрицательных элементов во втором массиве равны."
                    : "Количество отрицательных элементов во втором массиве больше чем количество положительных элементов в первом массиве.";

        Console.WriteLine(resultText);
    }
}
