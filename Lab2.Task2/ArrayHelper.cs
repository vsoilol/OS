using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Task2;

public class ArrayHelper
{
    public static int[] CreateArray(int arraySize)
    {
        int[] array = new int[arraySize];

        Random rand = new Random();

        for (int i = 0; i < arraySize; i++)
        {
            array[i] = rand.Next(-99, 99);
        }
        return array;
    }

    public static string ToString(int[] array)
    {
        var result = String.Join(", ", array);
        return result;
    }
}
