using System;
using System.IO.Pipes;
using System.Text;
using System.Xml.Linq;

class Program
{
    static int[,] matrix = new int[4, 3]
    {
        { -1, 2, 3 },
        { 4, -5, 6 },
        { -7, 8, -9 },
        { 10, 8, -9 }
    };

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Create a new thread to listen for client connections
        Thread serverThread = new Thread(ServerThreadProc);
        serverThread.Start();

        var rowsNumber = matrix.GetLength(0);

        for (var i = 0; i < rowsNumber; i++)
        {
            var client = new Thread(ClientThreadProc);
            client.Start();
        }

        // Wait for the server thread to exit
        serverThread.Join();
    }

    static void ClientThreadProc()
    {
        // Connect to the server pipe
        using (var client = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut))
        {
            client.Connect();

            int rowNumber = GetIntFromNamedPipeServerStream(client);

            var array = GetArrayFromTwoDArrayByRow(matrix, rowNumber);
            var positiveElementsCount = array.Count(_ => _ > 0);

            SendIntToNamedPipeServerStream(client, positiveElementsCount);
        }
    }

    static void ServerThreadProc()
    {
        var rowsNumber = matrix.GetLength(0);
        var minimumPositiveRow = 0;
        var minimumPositiveCount = 0;

        // Create a named pipe server stream
        using (var server = new NamedPipeServerStream("testpipe", PipeDirection.InOut))
        {
            for (var row = 0; row < rowsNumber; row++)
            {
                server.WaitForConnection();

                SendIntToNamedPipeServerStream(server, row);

                int positiveElementsCount = GetIntFromNamedPipeServerStream(server);

                if (minimumPositiveCount > positiveElementsCount || minimumPositiveCount == 0)
                {
                    minimumPositiveRow = row;
                    minimumPositiveCount = positiveElementsCount;
                }

                server.Disconnect();
            }
        }

        Console.WriteLine(
            $"Строка матрицы содержащей минимальное количество положительных элементов: {minimumPositiveRow + 1}"
        );
    }

    static int[] GetArrayFromTwoDArrayByRow(int[,] array2D, int row)
    {
        int[] array1D = new int[array2D.GetLength(1)];
        for (int i = 0; i < array2D.GetLength(1); i++)
        {
            array1D[i] = array2D[row, i];
        }

        return array1D;
    }

    static int GetIntFromNamedPipeServerStream(PipeStream namedPipe)
    {
        byte[] bytes = new byte[sizeof(int)];
        namedPipe.Read(bytes, 0, bytes.Length);
        int value = BitConverter.ToInt32(bytes, 0);

        return value;
    }

    static void SendIntToNamedPipeServerStream(PipeStream namedPipe, int value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        namedPipe.Write(bytes, 0, bytes.Length);
    }
}
