namespace Lab2.Task3;

public static class ArrayManipulator
{
    public static int[] GenerateRandomIntArray(int arraySize)
    {
        int[] array = new int[arraySize];
        Random rand = new Random();

        for (int i = 0; i < arraySize; i++)
        {
            array[i] = rand.Next(-99, 99);
        }
        return array;
    }

    public static string ArrayToString(int[] array)
    {
        var result = String.Join(", ", array);
        return result;
    }

    public static int GetSumOfPositiveElements(int[] array)
    {
        int sumOfPositive = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > 0)
            {
                sumOfPositive += array[i];
            }
        }

        return sumOfPositive;
    }

    public static int CountPositiveElements(int[] array)
    {
        int count = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > 0)
            {
                count++;
            }
        }

        return count;
    }

    public static int CountNegativeElements(int[] array)
    {
        int count = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < 0)
            {
                count++;
            }
        }

        return count;
    }
}
